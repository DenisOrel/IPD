
// Type: Intermech.Navigator.Selections.FilteredObjectsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Queries;
using Intermech.Remoting;
using System;
using System.Data;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Запрос списка объектов с отключением фильтрации Consts.NoFilterQuery
/// </summary>
/// <summary>
/// Конструктор запроса, в результате выполнения которого будет прочитана
/// информация о всех объектах указанного типа и производных от него,
/// которые удовлетворяют указанным условиям.
/// </summary>
/// <param name="support"></param>
/// <param name="objTypeID">Идентификатор типа объекта</param>
/// <param name="conditions">Массив условий, которым должны удовлетворять объекты</param>
/// <param name="services"></param>
internal sealed class FilteredObjectsQuery(
  INodeQuerySupport support,
  int objTypeID,
  ConditionStructure[] conditions,
  IServiceProvider services) : ObjectsQuery(support, objTypeID, conditions, services)
{
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    RemotingCallContext.SetData("X-IPS-NoFilterQuery", "true");
    try
    {
      return base.GetDataTable(queryParams);
    }
    finally
    {
      RemotingCallContext.FreeNamedDataSlot("X-IPS-NoFilterQuery");
    }
  }
}
