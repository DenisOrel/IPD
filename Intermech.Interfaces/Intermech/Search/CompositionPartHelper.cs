
// Type: Intermech.Search.CompositionPartHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using Intermech.Search.Data;
using Intermech.Search.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Search
{
    public static class CompositionPartHelper
    {
      public static IEnumerable<CompositionPart> CreateCompositionPartsFromDataTable(
        DataTable dataTable,
        DBRecordSetParams recordSetParams)
      {
        if (dataTable == null)
          throw new ArgumentNullException(nameof (dataTable));
        RecordSetParamsAdapter relationRecordSetParamsAdapter = new RecordSetParamsAdapter(recordSetParams, AttributeSourceTypes.Relation);
        RecordSetParamsAdapter objectRecordSetParamsAdapter = new RecordSetParamsAdapter(recordSetParams, AttributeSourceTypes.Object);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          yield return CompositionPartHelper.CreateCompositionPartFromDataRow(row, relationRecordSetParamsAdapter, objectRecordSetParamsAdapter);
      }

      public static CompositionPart CreateCompositionPartFromDataRow(
        DataRow dataRow,
        RecordSetParamsAdapter relationRecordSetParamsAdapter,
        RecordSetParamsAdapter objectRecordSetParamsAdapter)
      {
        if (dataRow == null)
          throw new ArgumentNullException(nameof (dataRow));
        if (relationRecordSetParamsAdapter == null)
          throw new ArgumentNullException(nameof (relationRecordSetParamsAdapter));
        if (objectRecordSetParamsAdapter == null)
          throw new ArgumentNullException(nameof (objectRecordSetParamsAdapter));
        IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
        return new CompositionPart(new Relation((IAttributeCollection) new AttributeCollectionDataRowAdapter(dataRow, (IRecordSetParamsAdapter) relationRecordSetParamsAdapter, attributeValueConverter)), new _Object((IAttributeCollection) new AttributeCollectionDataRowAdapter(dataRow, (IRecordSetParamsAdapter) objectRecordSetParamsAdapter, attributeValueConverter)));
      }
    }
}
