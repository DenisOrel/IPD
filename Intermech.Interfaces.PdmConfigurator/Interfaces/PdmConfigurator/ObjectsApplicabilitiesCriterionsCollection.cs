// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.ObjectsApplicabilitiesCriterionsCollection
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using Intermech.Kernel.Search;
using Intermech.Search;
using Intermech.Search.Data;
using Intermech.Search.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Коллекция критериев условия применения объекта в конфигураторе составов IPS
/// 
/// [!] Информация хранится в атрибуте связи "Условия применения объекта",
/// назначаемому конфигурируемым типам связей
/// </summary>
[Serializable]
public sealed class ObjectsApplicabilitiesCriterionsCollection : PdmCriterionsCollection
{
  /// <summary>
  /// Создать пустую коллекцию критериев условия применения объекта в конфигураторе составов IPS
  /// </summary>
  public ObjectsApplicabilitiesCriterionsCollection()
  {
  }

  /// <summary>
  /// Создать пустую коллекцию критериев условия применения объекта в конфигураторе составов IPS
  /// </summary>
  /// <param name="function">Логическая функция для объединения данной коллекции со следующим критерием/коллекцией</param>
  public ObjectsApplicabilitiesCriterionsCollection(LogicalFunction function)
    : base(function)
  {
  }

  /// <summary>
  /// Создать коллекцию критериев условия применения объекта в конфигураторе составов IPS на основе указанного объекта
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public ObjectsApplicabilitiesCriterionsCollection(object source)
    : base(source)
  {
  }

  public override bool SaveToObject(IDBAttributable obj)
  {
    if (!base.SaveToObject(obj))
      return false;
    if (!(obj is IDBRelation))
      return true;
    IDBRelation dbRelation = obj as IDBRelation;
    IDBAttribute attributeByGuid1 = dbRelation.GetAttributeByGuid(new Guid("cad001c0-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 == null)
      return true;
    IDBAttribute attributeByGuid2 = dbRelation.GetAttributeByGuid(new Guid("cad001c1-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid2 == null)
      return true;
    IDBRelationCollection relationCollection = obj.Session.GetRelationCollection(dbRelation.TypeID);
    relationCollection.FiltrationOwnerID = "cad001e0-306c-11d8-b4e9-00304f19f545";
    ColumnInfo[] source = new ColumnInfo[4]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, (object) null),
      new ColumnInfo((object) new Guid("cad001c0-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, (object) null),
      new ColumnInfo((object) new Guid("cad001c1-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, (object) null),
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null)
    };
    DBRecordSetParams dbRecordSetParams = new DBRecordSetParams()
    {
      Columns = ((IEnumerable<ColumnInfo>) source).Select<ColumnInfo, object>((System.Func<ColumnInfo, object>) (o => o.AttributeID)).ToArray<object>(),
      ColumnsInfo = source
    };
    DataTable dataTable = relationCollection.Select(dbRecordSetParams, dbRelation.ProjID, -1L, DateTime.Now);
    RecordSetParamsAdapter params1 = new RecordSetParamsAdapter(dbRecordSetParams, AttributeSourceTypes.Relation);
    RecordSetParamsAdapter params2 = new RecordSetParamsAdapter(dbRecordSetParams, AttributeSourceTypes.Object);
    IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      CompositionPart compositionPart = new CompositionPart(new Relation((IAttributeCollection) new AttributeCollectionDataRowAdapter(row, (IRecordSetParamsAdapter) params1, attributeValueConverter)), new _Object((IAttributeCollection) new AttributeCollectionDataRowAdapter(row, (IRecordSetParamsAdapter) params2, attributeValueConverter)));
      if (compositionPart.Relation.ID != dbRelation.RelationID)
      {
        object attributeValue1 = compositionPart.Relation.Attributes.GetAttributeValue(MetaDataHelper.GetAttributeID((object) new Guid("cad001c0-306c-11d8-b4e9-00304f19f545")));
        object attributeValue2 = compositionPart.Relation.Attributes.GetAttributeValue(MetaDataHelper.GetAttributeID((object) new Guid("cad001c1-306c-11d8-b4e9-00304f19f545")));
        if (attributeValue1 != null && attributeValue2 != null && attributeByGuid1.AsInteger == (long) attributeValue1 && attributeByGuid2.AsInteger == (long) attributeValue2)
          base.SaveToObject((IDBAttributable) dbRelation.Session.GetRelation(compositionPart.Relation.ID));
      }
    }
    return true;
  }
}
