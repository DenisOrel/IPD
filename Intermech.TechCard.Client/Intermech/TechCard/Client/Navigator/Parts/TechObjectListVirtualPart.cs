// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Parts.TechObjectListVirtualPart
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Queries;
using System;
using System.Collections;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Parts;

/// <summary>
/// 
/// </summary>
public class TechObjectListVirtualPart : TechObjectListPart
{
  /// <summary>
  /// Создать экземпляр класса, указав тип объектов для поиска
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objectTypeId">Тип объектов, версии которых указаны в списке</param>
  /// <param name="expandNode"></param>
  public TechObjectListVirtualPart(
    IList objectIDs,
    IServiceProvider services,
    int objectTypeId,
    bool expandNode)
    : base(objectIDs, services, objectTypeId, expandNode)
  {
  }

  /// <summary>
  /// Создать экземпляр класса, указав тип объектов для поиска
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="conditionsProvider"></param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objectTypeId">Тип объектов, версии которых указаны в списке</param>
  /// <param name="expandNode"></param>
  public TechObjectListVirtualPart(
    IList objectIDs,
    IConditionsProvider conditionsProvider,
    IServiceProvider services,
    int objectTypeId,
    bool expandNode)
    : base(objectIDs, conditionsProvider, services, objectTypeId, expandNode)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="support"></param>
  /// <param name="objTypeId"></param>
  /// <param name="conditions"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  protected override INodeQuery GetObjectsQuery(
    INodeQuerySupport support,
    int objTypeId,
    ConditionStructure[] conditions,
    IServiceProvider services)
  {
    return (INodeQuery) new TechObjectVirtualQuery((INodeQuerySupport) this, objTypeId, conditions, services);
  }
}
