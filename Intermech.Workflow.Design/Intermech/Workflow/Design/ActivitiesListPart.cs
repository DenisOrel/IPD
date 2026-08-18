// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivitiesListPart
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Workflow.Design;

internal class ActivitiesListPart : RelatedObjectsPart
{
  private ActivitiesDescriptor _parent;
  private long _attachedObjectID;
  private ConditionStructure[] _condition;

  /// <param name="services">Контейнер сервисов</param>
  public ActivitiesListPart(
    ActivitiesDescriptor parent,
    long AttachObjectID,
    IServiceProvider services)
    : base(0, AttachObjectID, RelatedObjectsRole.Applicability, wfConsts.AttachmentRelationTypeID, services)
  {
    this._parent = parent;
    this._attachedObjectID = AttachObjectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._attachedObjectID);
      if (objectInfo.Empty)
        return;
      this._objTypeID = objectInfo.ObjectTypeID;
      this._condition = new ConditionStructure[1]
      {
        new ConditionStructure(sessionKeeper.Session.IdentHelper.CompositionVersionID, RelationalOperators.Equal, (object) this._attachedObjectID, LogicalOperators.NONE, 0, false)
      };
    }
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (this._condition != null)
      conditions = ConditionStructure.Join(conditions, this._condition);
    if (this._parent.ActivityTypesFilter != null)
      conditions = this._parent.ActivityTypesFilter.Count <= 0 ? ConditionStructure.Join(new ConditionStructure(-2, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, false), conditions) : ConditionStructure.Join(new ConditionStructure(-7, RelationalOperators.In, (object) this._parent.ActivityTypesFilter.ToArray(), LogicalOperators.NONE, 0, false), conditions);
    INodeQuery query = base.GetQuery(conditions);
    if (!(query is RelatedObjectsQuery relatedObjectsQuery))
      return query;
    relatedObjectsQuery.QueryFilter = (IRelatedObjectQueryFilterMode) new RelatedObjectQueryFilterMode(true, false);
    return query;
  }

  public override NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    Guid columnSchemeGuid1 = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    Guid columnSchemeGuid2 = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrProcessID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid1, (object) ObligatoryObjectAttributes.CAPTION));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrRecipID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrActivityResultID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrActivityStatusID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrStartedID));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrCompletedID));
    return defaultColumns;
  }
}
