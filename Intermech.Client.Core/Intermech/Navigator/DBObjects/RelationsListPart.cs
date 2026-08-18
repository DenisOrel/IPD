
// Type: Intermech.Navigator.DBObjects.RelationsListPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком связей, заданной в виде коллекции идентификаторов.
/// </summary>
public class RelationsListPart : RelatedPartBase
{
  private ConditionStructure[] _conditions;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objID"></param>
  /// <param name="prjlinkIDs"></param>
  /// <param name="services">Контейнер сервисов</param>
  public RelationsListPart(long objID, IList prjlinkIDs, IServiceProvider services)
    : base(services)
  {
    this._objID = objID;
    if (prjlinkIDs == null || prjlinkIDs.Count <= 0)
      return;
    object[] conditionValue = new object[prjlinkIDs.Count];
    prjlinkIDs.CopyTo((Array) conditionValue, 0);
    this._conditions = new ConditionStructure[2]
    {
      new ConditionStructure(-21, RelationalOperators.Equal, (object) this._objID, LogicalOperators.AND, 0, false),
      new ConditionStructure(-20, RelationalOperators.In, (object) conditionValue, LogicalOperators.NONE, 0, false)
    };
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею связей.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    return this._conditions == null ? (INodeQuery) null : (INodeQuery) new RelatedObjectsQuery((INodeQuerySupport) this, this._objID, this._objTypeID, RelatedObjectsRole.Composition, -1, ConditionStructure.Join(conditions, this._conditions));
  }
}
