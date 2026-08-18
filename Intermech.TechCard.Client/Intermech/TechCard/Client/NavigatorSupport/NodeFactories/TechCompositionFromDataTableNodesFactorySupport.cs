// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.NavigatorSupport.NodeFactories.TechCompositionFromDataTableNodesFactorySupport
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.NavigatorSupport.NodeFactories;

/// <summary>
/// Поддержка фабрики узлов для построения дерева на основании данных о составе из DataTable
/// </summary>
internal class TechCompositionFromDataTableNodesFactorySupport : INodesFactorySupported
{
  /// <summary>
  /// 
  /// </summary>
  private readonly INodesFactory _nodesFactory;

  /// <summary>Конструктор</summary>
  /// <param name="dataTable">Таблица с данными иерархии объектов</param>
  /// <remarks>В таблице наличие полей F_PROJ_ID и F_PART_OBJ_ID обязательно!!</remarks>
  /// &gt;
  public TechCompositionFromDataTableNodesFactorySupport(DataTable dataTable)
  {
    this._nodesFactory = dataTable != null ? (INodesFactory) new TechCompositionFromDataTableNodesFactory(dataTable) : throw new ArgumentNullException(nameof (dataTable));
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
