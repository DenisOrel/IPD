
// Type: Intermech.Navigator.ArticlesList.ArticlesListPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections;


namespace Intermech.Navigator.ArticlesList;

/// <summary>
/// Создать экземпляр класса, указав тип объектов для поиска
/// </summary>
/// <param name="objectIDs">Список идентификаторов версий объекта</param>
/// <param name="services">Контейнер сервисов</param>
/// <param name="objectTypeID">Тип объектов, версии которых указаны в списке</param>
/// <param name="expandNode"></param>
internal sealed class ArticlesListPart(
  IList objectIDs,
  IConditionsProvider conditionsProvider,
  IServiceProvider services,
  int objectTypeID) : ObjectsListPart(objectIDs, conditionsProvider, services, objectTypeID, false)
{
  protected override ObjectNode GetNonExpandedNode(NodeID objNodeID)
  {
    return objNodeID == null ? (ObjectNode) null : (ObjectNode) new ArticleNonExpandedNode(objNodeID.TypeID, objNodeID.ObjectID);
  }

  protected override INodeQuery GetObjectsQuery(
    INodeQuerySupport support,
    int objTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
  {
    return (INodeQuery) new ArticlesListQuery(support, objTypeID, conditions, services);
  }
}
