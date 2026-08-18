
// Type: Intermech.Search.NotificationSelections.ExcludedObjectsNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.NotificationSelections;

public sealed class ExcludedObjectsNodePart(long[] objectVersionIds) : ObjectsListPart((IList) Enumerable.ToList<long>((IEnumerable<long>) objectVersionIds), (IServiceProvider) ServicesManager.ServiceContainer, -1, false)
{
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    base.GetQuery(conditions);
    if (this._objectIDs != null && this._objectIDs.Count > 0)
    {
      ConditionStructure[] conditionStructureArray = new ConditionStructure[3];
      ConditionStructure conditionStructure = new ConditionStructure();
      conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
      conditionStructure.RelationalOperator = RelationalOperators.In;
      conditionStructure.Value = (object) this._objectIDs.Cast<long>().ToArray<long>();
      conditionStructure.LogicalOperator = LogicalOperators.OR;
      conditionStructure.SQL = string.Empty;
      conditionStructureArray[0] = conditionStructure;
      conditionStructure = new ConditionStructure();
      conditionStructure.GroupID = 1;
      conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
      conditionStructure.RelationalOperator = RelationalOperators.In;
      conditionStructure.Value = (object) this._objectIDs.Cast<long>().ToArray<long>();
      conditionStructure.LogicalOperator = LogicalOperators.AND;
      conditionStructure.SQL = string.Empty;
      conditionStructureArray[1] = conditionStructure;
      conditionStructure = new ConditionStructure();
      conditionStructure.GroupID = -1;
      conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_LEVEL_ID;
      conditionStructure.RelationalOperator = RelationalOperators.Equal;
      conditionStructure.Value = (object) MetaDataHelper.GetLCLevelID("cad0000e-306c-11d8-b4e9-00304f19f545");
      conditionStructure.LogicalOperator = LogicalOperators.AND;
      conditionStructure.SQL = string.Empty;
      conditionStructureArray[2] = conditionStructure;
      this._conditions = conditionStructureArray;
    }
    else
      this._conditions = (ConditionStructure[]) null;
    IServiceProvider services = this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null;
    return this._conditions == null ? (INodeQuery) null : this.GetObjectsQuery((INodeQuerySupport) this, -1, ConditionStructure.Join(conditions, this._conditions), services);
  }
}
