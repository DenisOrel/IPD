
// Type: Intermech.Interfaces.Compositions.CompositionService.RelInfoDbScheme
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Interfaces.Compositions.CompositionService
{
    /// <summary>
    /// Класс схемы / модели для загрузки данных объектов типа RelInfoItem
    /// </summary>
    public class RelInfoDbScheme : ObjectDbScheme<RelInfoItem>
    {
      /// <summary>
      /// 
      /// </summary>
      private readonly string _relationIdField;
      /// <summary>
      /// 
      /// </summary>
      private readonly string _relationTypeField;
      /// <summary>
      /// 
      /// </summary>
      private readonly int _relationIdFieldIndex;
      /// <summary>
      /// 
      /// </summary>
      private readonly int _relationTypeFieldIndex;
      /// <summary>
      /// 
      /// </summary>
      private readonly bool _columnIndexMode;
      /// <summary>
      /// 
      /// </summary>
      private static IEnumerable<ColumnDescriptor> _columns;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="relationIdField"></param>
      /// <param name="relationTypeField"></param>
      public RelInfoDbScheme(string relationIdField = "F_PRJLINK_ID", string relationTypeField = "F_RELATION_TYPE")
      {
        this._relationIdField = relationIdField;
        this._relationTypeField = relationTypeField;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="relationIdFieldIndex"></param>
      /// <param name="relationTypeFieldIndex"></param>
      public RelInfoDbScheme(int relationIdFieldIndex, int relationTypeFieldIndex)
      {
        if (relationIdFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (relationIdFieldIndex));
        if (relationTypeFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (relationTypeFieldIndex));
        this._relationIdFieldIndex = relationIdFieldIndex;
        this._relationTypeFieldIndex = relationTypeFieldIndex;
        this._columnIndexMode = true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataRow"></param>
      /// <returns></returns>
      public override RelInfoItem ParseItem(DataRow dataRow)
      {
        return !this._columnIndexMode ? new RelInfoItem(DataSetProcessor.GetInt64Value(dataRow, this._relationIdField, 0L), DataSetProcessor.GetInt32Value(dataRow, this._relationTypeField, -1)) : new RelInfoItem(DataSetProcessor.GetInt64Value(dataRow, this._relationIdFieldIndex, 0L), DataSetProcessor.GetInt32Value(dataRow, this._relationTypeFieldIndex, -1));
      }

      /// <summary>
      /// Получение списка полей таблицы, для заполнения объектов
      /// </summary>
      /// <returns></returns>
      public static IEnumerable<ColumnDescriptor> GetSourceTableColumns()
      {
        if (RelInfoDbScheme._columns == null)
          RelInfoDbScheme._columns = (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
          {
            new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
          };
        return (IEnumerable<ColumnDescriptor>) RelInfoDbScheme._columns.ToArray<ColumnDescriptor>();
      }
    }
}
