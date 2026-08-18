
// Type: Intermech.Interfaces.Compositions.AutomaticSorting.CompositionSortingInfoDbScheme
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Interfaces.Compositions.AutomaticSorting
{
    /// <summary>
    /// Класс схемы / модели для загрузки данных объектов типа
    /// </summary>
    public class CompositionSortingInfoDbScheme : ObjectDbScheme<CompositionSortingInfoItem>
    {
      /// <summary>
      /// 
      /// </summary>
      private readonly string _relationIdField;
      private readonly string _relationTypeField;
      private readonly string _projObjIdField;
      private readonly string _partObjTypeField;
      private readonly string _sortingField;
      /// <summary>
      /// 
      /// </summary>
      private static IEnumerable<ColumnDescriptor> _columns;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="relationIdField"></param>
      /// <param name="relationTypeField"></param>
      /// <param name="projObjIdField"></param>
      /// <param name="partObjTypeField"></param>
      /// <param name="sortingValue"></param>
      public CompositionSortingInfoDbScheme(
        string relationIdField = "F_PRJLINK_ID",
        string relationTypeField = "F_RELATION_TYPE",
        string projObjIdField = "F_PROJ_ID",
        string partObjTypeField = "F_OBJECT_TYPE",
        string sortingValue = "cad00202-306c-11d8-b4e9-00304f19f545")
      {
        this._relationIdField = relationIdField;
        this._relationTypeField = relationTypeField;
        this._projObjIdField = projObjIdField;
        this._partObjTypeField = partObjTypeField;
        this._sortingField = sortingValue;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataRow"></param>
      /// <returns></returns>
      public override CompositionSortingInfoItem ParseItem(DataRow dataRow)
      {
        return new CompositionSortingInfoItem(DataSetProcessor.GetInt64Value(dataRow, this._relationIdField, 0L), DataSetProcessor.GetInt32Value(dataRow, this._relationTypeField, -1), DataSetProcessor.GetInt32Value(dataRow, this._partObjTypeField, -1), DataSetProcessor.GetInt64Value(dataRow, this._sortingField, -1L));
      }

      /// <summary>
      /// Получение списка полей таблицы, для заполнения объектов
      /// </summary>
      /// <returns></returns>
      public static IEnumerable<ColumnDescriptor> GetSourceTableColumns()
      {
        if (CompositionSortingInfoDbScheme._columns == null)
          CompositionSortingInfoDbScheme._columns = (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>(4)
          {
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
          };
        return (IEnumerable<ColumnDescriptor>) CompositionSortingInfoDbScheme._columns.ToArray<ColumnDescriptor>();
      }
    }
}
