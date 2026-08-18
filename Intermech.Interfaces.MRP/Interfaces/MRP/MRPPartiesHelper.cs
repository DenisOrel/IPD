// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPPartiesHelper
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Kernel.Search;
using Intermech.Pdm.InstancesAndParties;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Вспомогательный статический класс, позволяющий отыскивать партии изделий по указанным критериям
/// </summary>
public static class MRPPartiesHelper
{
  /// <summary>Колонки для работы с партиями изделий</summary>
  private static object[] objectColumns = new object[4]
  {
    (object) ObligatoryObjectAttributes.F_OBJECT_ID,
    (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
    (object) -1,
    (object) -1
  };

  /// <summary>
  /// Отыскать партию для указанного изделия с учётом его признака изготовления
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="article">Описание изделия</param>
  /// <param name="isBoughtArticle">Признак изготовления изделия</param>
  /// <returns>Найденная партия или null</returns>
  public static IMRPTypedObjectRef FindParty(
    IUserSession session,
    IServiceProvider services,
    IMRPTypedObjectRef article,
    long isBoughtArticle)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (article == null || article.ObjectID == 0L)
      throw new ArgumentNullException(nameof (article));
    IDBObjectCollection objectCollection = session.GetObjectCollection(InstancePartyObjectType4ObjectTypeHelper.GetPartyObjectTypeID4ObjectTypeID(session, article.TypeID));
    objectCollection.ShowAllModifications = false;
    MRPPartiesHelper.objectColumns[2] = (object) MetaDataHelper.GetAttributeTypeID("cad0038f-306c-11d8-b4e9-00304f19f545");
    MRPPartiesHelper.objectColumns[3] = (object) MetaDataHelper.GetAttributeTypeID("cadd93c3-306c-11d8-b4e9-00304f19f545");
    string orderNumber = MRPContextHelper.GetOrderNumber((IMRPContext) new MRPContext(services));
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(2);
    conditionStructureList.Add(new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad00622-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) Math.Abs(article.ObjectID), LogicalOperators.AND, 0, true));
    if (!string.IsNullOrEmpty(orderNumber))
      conditionStructureList.Add(new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cadd93c3-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) orderNumber, LogicalOperators.AND, 0, true));
    DataTable dataTable = objectCollection.Select(new DBRecordSetParams(conditionStructureList.ToArray(), MRPPartiesHelper.objectColumns));
    if (dataTable == null)
      return (IMRPTypedObjectRef) null;
    try
    {
      if (dataTable.Rows.Count == 0)
        return (IMRPTypedObjectRef) null;
      MRPTypedObjectRef party = (MRPTypedObjectRef) null;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if (DataSetProcessor.GetInt64Value(dataTable.Rows[index][2], 1L) == isBoughtArticle)
        {
          party = new MRPTypedObjectRef(services, DataSetProcessor.GetInt64Value(dataTable.Rows[index][0], 0L), Guid.Empty, DataSetProcessor.GetInt32Value(dataTable.Rows[index][1], -1));
          break;
        }
      }
      return (IMRPTypedObjectRef) party;
    }
    finally
    {
      dataTable.Dispose();
    }
  }
}
