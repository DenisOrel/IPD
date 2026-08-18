
// Type: Intermech.Interfaces.Compositions.CompositionService.ObjInfoDbScheme
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
    public class ObjInfoDbScheme : ObjectDbScheme<ObjInfoItem>
    {
      /// <summary>
      /// 
      /// </summary>
      private readonly string _objectIdField;
      /// <summary>
      /// 
      /// </summary>
      private readonly string _objectTypeField;
      /// <summary>
      /// 
      /// </summary>
      private readonly int _objectIdFieldIndex;
      /// <summary>
      /// 
      /// </summary>
      private readonly int _objectTypeFieldIndex;
      /// <summary>
      /// 
      /// </summary>
      protected readonly bool _columnIndexMode;
      /// <summary>
      /// 
      /// </summary>
      private static IEnumerable<ColumnDescriptor> _columns;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objectIdField"></param>
      /// <param name="objectTypeField"></param>
      public ObjInfoDbScheme(string objectIdField = "F_OBJECT_ID", string objectTypeField = "F_OBJECT_TYPE")
      {
        this._objectIdField = objectIdField;
        this._objectTypeField = objectTypeField;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="objectIdFieldIndex"></param>
      /// <param name="objectTypeFieldIndex"></param>
      public ObjInfoDbScheme(int objectIdFieldIndex, int objectTypeFieldIndex)
      {
        if (objectIdFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (objectIdFieldIndex));
        if (objectTypeFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (objectTypeFieldIndex));
        this._objectIdFieldIndex = objectIdFieldIndex;
        this._objectTypeFieldIndex = objectTypeFieldIndex;
        this._columnIndexMode = true;
      }

      /// <summary>Создание / загрузка содержимого объекта</summary>
      /// <param name="dataRow"></param>
      /// <returns></returns>
      public override ObjInfoItem ParseItem(DataRow dataRow)
      {
        return !this._columnIndexMode ? new ObjInfoItem(DataSetProcessor.GetInt64Value(dataRow, this._objectIdField, 0L), DataSetProcessor.GetInt32Value(dataRow, this._objectTypeField, -1)) : new ObjInfoItem(DataSetProcessor.GetInt64Value(dataRow, this._objectIdFieldIndex, 0L), DataSetProcessor.GetInt32Value(dataRow, this._objectTypeFieldIndex, -1));
      }

      /// <summary>
      /// Получение списка полей таблицы, для заполнения объектов
      /// </summary>
      /// <returns></returns>
      public static IEnumerable<ColumnDescriptor> GetSourceTableColumns()
      {
        if (ObjInfoDbScheme._columns != null)
          return (IEnumerable<ColumnDescriptor>) ObjInfoDbScheme._columns.ToArray<ColumnDescriptor>();
        return (IEnumerable<ColumnDescriptor>) (ColumnDescriptor[]) (ObjInfoDbScheme._columns = (IEnumerable<ColumnDescriptor>) new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
        });
      }
    }
}
