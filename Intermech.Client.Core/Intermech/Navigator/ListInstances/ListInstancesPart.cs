
// Type: Intermech.Navigator.ListInstances.ListInstancesPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.ListInstances;

internal sealed class ListInstancesPart : ObjectsPart
{
  /// <summary>Значение атрибута "Идентификатор группового изделия"</summary>
  private IListInstancesInfo _info;
  /// <summary>Список идентификаторов исполнений</summary>
  internal List<long> InstancesIDs;

  public ListInstancesPart(IListInstancesInfo info, IServiceProvider services)
    : base(services)
  {
    this._info = info;
    this.InstancesIDs = new List<long>(1);
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (this._info == null)
      return (INodeQuery) null;
    if (this._info.NumGroupInstance == Guid.Empty)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._info.InitInstanceGUID, false);
        if (dbObject != null)
        {
          IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"));
          if (attributeByGuid != null)
          {
            if (GuidHelper.IsGuid(attributeByGuid.AsString))
              this._info.NumGroupInstance = new Guid(attributeByGuid.AsString);
          }
        }
      }
    }
    return (INodeQuery) new ListInstancesQuery((INodeQuerySupport) this, -1, ConditionStructure.Join(this._info.NumGroupInstance != Guid.Empty ? new ConditionStructure(new Guid("cad001f9-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) this._info.NumGroupInstance, LogicalOperators.AND, 0) : new ConditionStructure(-12, RelationalOperators.Equal, (object) this._info.InitInstanceGUID, LogicalOperators.AND, 0, false), conditions), this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null);
  }
}
