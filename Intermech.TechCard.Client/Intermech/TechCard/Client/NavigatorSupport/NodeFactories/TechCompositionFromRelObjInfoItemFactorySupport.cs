// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.NavigatorSupport.NodeFactories.TechCompositionFromRelObjInfoItemFactorySupport
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.NavigatorSupport.NodeFactories;

/// <summary>
/// Поддержка фабрики узлов для построения дерева на основании данных о составе из DataTable
/// </summary>
internal class TechCompositionFromRelObjInfoItemFactorySupport : INodesFactorySupported
{
  /// <summary>
  /// 
  /// </summary>
  private readonly INodesFactory _nodesFactory;

  /// <summary>Конструктор</summary>
  /// <param name="relObjInfoItems">Описание с данными иерархии объектов</param>
  /// <remarks></remarks>
  public TechCompositionFromRelObjInfoItemFactorySupport(
    IEnumerable<RelObjInfoItem> relObjInfoItems,
    bool composition = true)
  {
    this._nodesFactory = relObjInfoItems != null ? (INodesFactory) new TechCompositionFromRelObjInfoItemFactory(relObjInfoItems, composition) : throw new ArgumentNullException(nameof (relObjInfoItems));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  public INodesFactory GetNodesFactory(IServiceProvider services, INodeID nodeId)
  {
    return this._nodesFactory;
  }
}
