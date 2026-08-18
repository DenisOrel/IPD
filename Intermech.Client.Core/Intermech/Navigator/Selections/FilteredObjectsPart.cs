
// Type: Intermech.Navigator.Selections.FilteredObjectsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Неотфильтрованный по Consts.NoFilterQuery список объектов
/// </summary>
/// <summary>
/// Конструктор части, позволяющий указать провайдер динамически изменяющихся
/// условий, которым должны удовлетворять объекты.
/// </summary>
/// <param name="conditionsProvider">Провайдер условий.</param>
/// <param name="services">Контейнер сервисов</param>
internal sealed class FilteredObjectsPart(
  IConditionsProvider conditionsProvider,
  IServiceProvider services) : ObjectsPart(conditionsProvider, services)
{
  protected override ObjectsQuery GetObjectsQuery(
    ConditionStructure[] conditions,
    IServiceProvider services)
  {
    return (ObjectsQuery) new FilteredObjectsQuery((INodeQuerySupport) this, this.objTypeID, conditions, services);
  }
}
