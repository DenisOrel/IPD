// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Parts.TechObjectListPart
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.TechCard.Client.Navigator.Nodes;
using Intermech.TechCard.Client.Navigator.Queries;
using System;
using System.Collections;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Parts;

/// <summary>
/// 
/// </summary>
public class TechObjectListPart : ObjectsListPart
{
  /// <summary>
  /// Составное значение: атрибут F_PRJLINK_ID : источник - связь (special for fake field)
  /// </summary>
  public static NodeColumnID ncF_PRJLINK_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectIDs"></param>
  /// <param name="services">Контейнер сервисов</param>
  public TechObjectListPart(IList objectIDs, IServiceProvider services)
    : base(objectIDs, services)
  {
  }

  /// <summary>
  /// Создать экземпляр класса, указав тип объектов для поиска
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objectTypeId">Тип объектов, версии которых указаны в списке</param>
  public TechObjectListPart(IList objectIDs, IServiceProvider services, int objectTypeId)
    : base(objectIDs, services, objectTypeId)
  {
  }

  /// <summary>
  /// Создать экземпляр класса, указав тип объектов для поиска
  /// </summary>
  /// <param name="objectIDs">Список идентификаторов версий объекта</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="objectTypeId">Тип объектов, версии которых указаны в списке</param>
  /// <param name="expandNode"></param>
  public TechObjectListPart(
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
  public TechObjectListPart(
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
  /// <param name="columnSetName"></param>
  /// <returns></returns>
  public override NodeColumnCollection GetSupportedColumns(string columnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Intermech.Navigator.DBObjects.Helper.AddObjectTypeColumns(columns, this.ObjectTypeID);
    this.GetSupportedColumns(columnSetName, columns);
    return columns;
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
    return (INodeQuery) new TechObjectQuery((INodeQuerySupport) this, objTypeId, conditions, services);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fieldValues"></param>
  /// <param name="adapter"></param>
  /// <returns></returns>
  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string str = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    int fieldIndex = adapter.GetFieldIndex((object) TechObjectListPart.ncF_PRJLINK_ID);
    long num = fieldIndex != -1 ? Convert.ToInt64(fieldValues[fieldIndex]) : -1L;
    long int64_4 = adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER) >= 0 ? Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]) : 0L;
    long int64_5 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)] == DBNull.Value ? 0L : Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)]);
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    long prjLinkId = num;
    int lcStepID = int32_2;
    string caption = str;
    long owner = int64_4;
    long sorting = int64_5;
    Guid empty = Guid.Empty;
    return (INodeID) new TechNodeID(int32_1, objId, id, checkedOutBy, prjLinkId, lcStepID, caption, -1, owner, sorting, 0L, empty, 0L);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeId"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeId) => base.GetChild(nodeId);
}
