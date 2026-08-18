
// Type: Intermech.Navigator.DBObjects.SelectObjectsListPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;


namespace Intermech.Navigator.DBObjects;

/// <summary>Список объектов</summary>
public sealed class SelectObjectsListPart : ObjectsListPart, IContextAware
{
  /// <summary>Контейнер сервисов</summary>
  protected IServiceProvider services;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectIDs">Список идентификаторов версий объектов</param>
  /// <param name="services"></param>
  public SelectObjectsListPart(IList objectIDs, IServiceProvider services)
    : base(objectIDs, services)
  {
    this.services = services;
  }

  /// <summary>Получить дочерний узел по его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел по его описанию</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return this.services != null && this.services.GetService(typeof (NavigatorTreeView)) == null ? base.GetChild(nodeID) : (INode) new CompositeNode();
  }

  public override IServiceProvider Services
  {
    get => this.services;
    set => this.services = value;
  }
}
