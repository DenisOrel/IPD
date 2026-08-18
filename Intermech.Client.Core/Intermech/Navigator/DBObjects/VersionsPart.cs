
// Type: Intermech.Navigator.DBObjects.VersionsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Navigator.DBObjects;

internal sealed class VersionsPart : ObjectsPart
{
  private readonly long _objectID;
  private readonly long _id;
  private readonly VersionsWindowVisualModes _mode;
  private readonly DateTime _currentDate;

  public VersionsPart(
    long objectID,
    long id,
    VersionsWindowVisualModes mode,
    DateTime onDate,
    IServiceProvider services)
    : base(services)
  {
    this._objectID = objectID;
    this._id = id;
    this._mode = mode;
    this._currentDate = onDate;
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (this._objectID == 0L)
      return (INodeQuery) null;
    if (this._currentDate != DateTime.MaxValue)
      conditions = ConditionStructure.Join(new ConditionStructure(-13, RelationalOperators.LessOrEqual, (object) this._currentDate, LogicalOperators.AND, 0, false), conditions);
    IServiceProvider services = this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null;
    conditions = ConditionStructure.Join(new ConditionStructure(-3, RelationalOperators.Equal, (object) this._id, LogicalOperators.AND, 0, false), conditions);
    if (this._mode == VersionsWindowVisualModes.TREE)
      conditions = ConditionStructure.Join(new ConditionStructure((string) null, RelationalOperators.ParentVersionID, (object) this._objectID, LogicalOperators.AND, 0, false), conditions);
    return (INodeQuery) new VersionsQuery((INodeQuerySupport) this, this._id, conditions, services);
  }

  public override NodeColumnCollection GetDefaultColumns()
  {
    return Utils.VersionColumns(NodeColumnSortOrder.None, this._mode == VersionsWindowVisualModes.LIST);
  }

  public override NodeColumnCollection GetSupportedColumns(string columnSetName)
  {
    return VersionsNode.VersionsTreeSupportedColumns(VersionsHelper.GetVersionsObjectTypes(this._id), this._mode);
  }

  public override INode GetChild(INodeID nodeID)
  {
    if (this._mode == VersionsWindowVisualModes.TREE)
      return (INode) new VersionsNode(((NodeID) nodeID).ObjectID, this._id, ((NodeID) nodeID).ObjectTypeID, this._mode, this._currentDate);
    return this._mode == VersionsWindowVisualModes.LIST ? (INode) new VersionListNode(((NodeID) nodeID).ObjectTypeID, ((NodeID) nodeID).ObjectID) : (INode) null;
  }
}
