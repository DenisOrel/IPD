
// Type: Intermech.Search.Navigator.ObjectsNodePart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Navigator;

public sealed class ObjectsNodePart : ObjectsPartBase
{
  private ConditionStructure[] _conditions;

  public static ObjectsNodePart CreateForObjects(
    long[] objectVersionIds,
    IServiceProvider serviceProvider)
  {
    if (objectVersionIds == null || objectVersionIds.Length == 0 || ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds))
      throw new ArgumentException();
    ConditionStructure[] conditions = new ConditionStructure[2];
    ConditionStructure conditionStructure = new ConditionStructure();
    conditionStructure.GroupID = 1;
    conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
    conditionStructure.RelationalOperator = RelationalOperators.In;
    conditionStructure.Value = (object) ((IEnumerable<long>) objectVersionIds).ToArray<long>();
    conditionStructure.LogicalOperator = LogicalOperators.OR;
    conditionStructure.SQL = string.Empty;
    conditions[0] = conditionStructure;
    conditionStructure = new ConditionStructure();
    conditionStructure.GroupID = -1;
    conditionStructure.Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
    conditionStructure.RelationalOperator = RelationalOperators.In;
    conditionStructure.Value = (object) ((IEnumerable<long>) objectVersionIds).Select<long, long>((Func<long, long>) (o => -o)).ToArray<long>();
    conditionStructure.SQL = string.Empty;
    conditions[1] = conditionStructure;
    return new ObjectsNodePart(conditions, serviceProvider);
  }

  public ObjectsNodePart(ConditionStructure[] conditions, IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
    this._conditions = conditions != null ? conditions : throw new ArgumentNullException(nameof (conditions));
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    return (INodeQuery) new ObjectsNodeQuery(ConditionStructure.Join(this._conditions, conditions), (INodeQuerySupport) this, this.Services);
  }
}
