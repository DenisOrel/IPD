// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Parts.TechCompositionPart
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode;
using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Nodes;
using Intermech.TechCard.Client.Navigator.Params;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Parts;

/// <summary>
/// 
/// </summary>
public class TechCompositionPart : AdvRelationsPart
{
  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  private readonly List<NodeColumnID> _attributes;

  /// <summary>Constructor</summary>
  /// <param name="projObjTypeId">Parent object type's id</param>
  /// <param name="projId">Parent object version's id</param>
  /// <param name="relationTypeId">Relation type's id</param>
  /// <param name="attributes">Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок</param>
  /// <param name="role"></param>
  /// <param name="filtrationOwnerId">Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
  /// <param name="contexts">Список контекстов, в рамках которых будет считываться состав</param>
  /// <param name="services">Контейнер сервисов</param>
  public TechCompositionPart(
    int projObjTypeId,
    long projId,
    int relationTypeId,
    List<NodeColumnID> attributes,
    RelatedObjectsRole role,
    string filtrationOwnerId,
    List<long> contexts,
    IServiceProvider services)
    : base(projObjTypeId, projId, relationTypeId, filtrationOwnerId, contexts, (List<int>) null, services)
  {
    this._role = role;
    this.ncAdvAttributes = attributes == null || attributes.Count <= 0 ? (List<NodeColumnID>) null : new List<NodeColumnID>(attributes.Count);
    this._attributes = attributes;
    if (attributes == null || this.ncAdvAttributes == null)
      return;
    foreach (object attribute in attributes)
      this.ncAdvAttributes.Add(new NodeColumnID(attribute, AttributeSourceTypes.Relation));
  }

  /// <summary>Get default columns</summary>
  /// <returns></returns>
  public override NodeColumnCollection GetDefaultColumns()
  {
    return this.GetSchemeDefaultColumns(base.GetDefaultColumns());
  }

  /// <summary>Get supported columns collection</summary>
  /// <param name="columnSetName"></param>
  /// <returns></returns>
  public override NodeColumnCollection GetSupportedColumns(string columnSetName)
  {
    return this.GetSchemeSupportedColumns(TechCardNavTreeViewUtils.GetObjAndRelSupportedColumns(this.Descriptor is TechCompositionBaseDescriptor descriptor ? descriptor.CompObjTypeID : this._objTypeID, this._relTypeID), columnSetName);
  }

  /// <summary>
  /// </summary>
  /// <param name="conditions"></param>
  /// <returns></returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (!(this.Descriptor is TechCompositionBaseDescriptor descriptor))
      return (INodeQuery) null;
    INodeQuery query = descriptor.CompositionFilter == null || descriptor.CompositionFilter.CallBaseMethod(this, ref conditions) ? base.GetQuery(conditions) : descriptor.CompositionFilter.GetCustomQuery(this, conditions);
    if (query is RelatedObjectsQuery relatedObjectsQuery)
    {
      IRelatedObjectQueryFilterMode queryFilter = descriptor.CompositionFilter?.QueryFilter;
      if (queryFilter != null)
        relatedObjectsQuery.QueryFilter = queryFilter;
    }
    return query;
  }

  /// <summary>Create node's description</summary>
  /// <param name="fieldValues"></param>
  /// <param name="adapter"></param>
  /// <returns></returns>
  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJLINK_ID)]);
    string caption = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    long int64_6 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64Value1 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING) >= 0 ? DataSetProcessor.GetInt64Value(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)], 0L) : 0L;
    long int64Value2 = adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION) >= 0 ? DataSetProcessor.GetInt64Value(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)], 0L) : 0L;
    long int64Value3 = adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION) >= 0 ? DataSetProcessor.GetInt64Value(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION)], 0L) : 0L;
    string siteID = adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID) >= 0 ? DataSetProcessor.GetStringValue(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)], string.Empty) : string.Empty;
    Guid relationGuid = adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJ_GUID) >= 0 ? DataSetProcessor.GetGuidValue(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJ_GUID)], Guid.Empty) : Guid.Empty;
    object[] values = this.ncAdvAttributes != null ? new object[this.ncAdvAttributes.Count] : (object[]) null;
    if (this.ncAdvAttributes != null && values != null)
    {
      for (int index = 0; index < this.ncAdvAttributes.Count; ++index)
      {
        int fieldIndex = adapter.GetFieldIndex((object) this.ncAdvAttributes[index]);
        values[index] = fieldIndex >= 0 ? fieldValues[fieldIndex] : (object) null;
        values[index] = values[index] != DBNull.Value ? values[index] : (object) null;
      }
    }
    return (INodeID) new TechCompositionNodeID((CreateObjectNodeParams) new CreateTechNodeParams(int32_1, int64_1, int64_2, int64_5, int64_3, int32_2, caption, this._relTypeID, int64_6, int64Value1, ObjectFiltrationState.fsNotRequired, int64Value2, int64Value3, siteID, int64_4, this._filtrationOwnerID, this._contexts, this._objTypeID, this._objID, relationGuid, this._attributes, values));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields();
    if (!specialFields.Contains((object) ObjectsPartBase.ncSORTING))
      specialFields.Add((object) ObjectsPartBase.ncSORTING);
    if (!specialFields.Contains((object) ObjectsPartBase.ncMODIFICATION_ID))
      specialFields.Add((object) ObjectsPartBase.ncMODIFICATION_ID);
    return specialFields;
  }

  /// <summary>Get child node by description</summary>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeId)
  {
    return nodeId is TechCompositionNodeID compositionNodeId ? (INode) new TechCompositionNode(this.Descriptor, compositionNodeId.Params) : base.GetChild(nodeId);
  }

  /// <summary>Object's ID</summary>
  public long ObjID => this._objID;

  /// <summary>Object's type ID</summary>
  public int ObjTypeID => this._objTypeID;

  /// <summary>Object's related role</summary>
  public RelatedObjectsRole ObjRole => this._role;

  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  public string FiltrationOwnerID => this._filtrationOwnerID;

  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  public List<long> Contexts => this._contexts;

  /// <summary>
  /// 
  /// </summary>
  public IDescriptor Descriptor
  {
    get => !(this.Owner is TechCompositionNode owner) ? (IDescriptor) null : owner.Descriptor;
  }
}
