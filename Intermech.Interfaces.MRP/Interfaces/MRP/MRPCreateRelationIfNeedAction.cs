// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCreateRelationIfNeedAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее создавать связь при её отсутствии
/// </summary>
public class MRPCreateRelationIfNeedAction : MRPCreateRelationActionBase
{
  /// <summary>
  /// Guid исходной связи, который должен быть записан на атрибуте найденной связи "Создана на основе связи"
  /// </summary>
  public Guid sourceRelGuid = Guid.Empty;
  /// <summary>Колонки для работы с партиями изделий</summary>
  private static object[] relationColumns = new object[3]
  {
    (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
    (object) ObligatoryObjectAttributes.F_PROJ_ID,
    (object) ObligatoryObjectAttributes.F_OBJECT_ID
  };

  /// <summary>
  /// Создать действие, позволяющее создавать связь при её отсутствии
  /// </summary>
  /// <param name="services">Контейнер сервисов (контест MRP)</param>
  /// <param name="projID">Описание родительского объекта</param>
  /// <param name="partID">Описание дочернего объекта</param>
  /// <param name="relTypeID">Тип создаваемой связи</param>
  /// <param name="sourceRelGuid">Guid исходной связи, который должен быть записан на атрибуте найденной связи "Создана на основе связи"</param>
  public MRPCreateRelationIfNeedAction(
    IServiceProvider services,
    IMRPTypedObjectRef projID,
    IMRPTypedObjectRef partID,
    int relTypeID,
    Guid sourceRelGuid)
    : base(services, projID, partID, relTypeID)
  {
    this.sourceRelGuid = sourceRelGuid;
  }

  /// <summary>
  /// Создать связь между указанной версией родительского объекта и указанной версией дочернего объекта
  /// </summary>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="partID">Идентификатор версии дочернего объекта</param>
  /// <param name="collection">Коллекция связей</param>
  /// <returns>Описание созданной связи</returns>
  protected override IDBRelation CreateRelation(
    long projID,
    long partID,
    IDBRelationCollection collection)
  {
    if (collection == null)
      throw new ArgumentNullException(nameof (collection));
    long aRelationID = 0;
    if (this.sourceRelGuid != Guid.Empty)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams(new List<ConditionStructure>(3)
      {
        new ConditionStructure(-21, RelationalOperators.Equal, (object) projID, LogicalOperators.AND, 0, true),
        new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cadd92ec-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) this.sourceRelGuid, LogicalOperators.AND, 0, true)
      }.ToArray(), MRPCreateRelationIfNeedAction.relationColumns);
      DataTable dataTable = collection.Select(paramSet);
      if (dataTable != null)
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64Value1 = DataSetProcessor.GetInt64Value(dataTable.Rows[index][0], 0L);
          long int64Value2 = DataSetProcessor.GetInt64Value(dataTable.Rows[index][1], 0L);
          long int64Value3 = DataSetProcessor.GetInt64Value(dataTable.Rows[index][2], 0L);
          long num = projID;
          if (int64Value2 == num && int64Value3 == partID)
          {
            aRelationID = int64Value1;
            break;
          }
        }
        dataTable.Dispose();
      }
    }
    IDBRelation relation = aRelationID != 0L ? collection.Session.GetRelation(aRelationID, true) : (this.sourceRelGuid == Guid.Empty ? collection.Session.GetRelation(projID, partID, collection.RelationTypeID, true) : (IDBRelation) null);
    if (relation == null)
    {
      MRPNavigatorEventsRef service = this.Services.GetService(typeof (MRPNavigatorEventsRef)) as MRPNavigatorEventsRef;
      try
      {
        relation = collection.Create(projID, partID);
        service?.AddCreatedRelation(relation.RelationID, relation.RelationType, relation.ProjID, -1);
        this.isNewRelation = true;
      }
      catch
      {
      }
    }
    return relation;
  }
}
