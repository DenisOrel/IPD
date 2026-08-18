// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AttachmentsListPart
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>
/// Создать экземпляр класса, указав тип объектов для поиска
/// </summary>
/// <param name="objectIDs">Список идентификаторов версий объекта</param>
/// <param name="services">Контейнер сервисов</param>
/// <param name="objectTypeID">Тип объектов, версии которых указаны в списке</param>
/// <param name="expandNode"></param>
internal sealed class AttachmentsListPart(
  IList objectIDs,
  IConditionsProvider conditionsProvider,
  IServiceProvider services,
  int objectTypeID,
  bool expandNode) : ObjectsListPart(objectIDs, conditionsProvider, services, objectTypeID, expandNode)
{
  protected override INodeQuery GetObjectsQuery(
    INodeQuerySupport support,
    int objTypeID,
    ConditionStructure[] conditions,
    IServiceProvider services)
  {
    return (INodeQuery) new AttachmentsObjectsQuery(support, objTypeID, conditions, services);
  }
}
