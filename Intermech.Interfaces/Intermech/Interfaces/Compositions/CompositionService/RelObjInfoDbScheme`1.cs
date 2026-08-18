
// Type: Intermech.Interfaces.Compositions.CompositionService.RelObjInfoDbScheme`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Interfaces.Compositions.CompositionService
{
    /// <summary>
    /// Класс схемы / модели для загрузки данных объектов типа RelObjInfoItem
    /// </summary>
    public class RelObjInfoDbScheme<T> : ObjectDbScheme<RelObjInfoItem> where T : ObjInfoItem, new()
    {
      private readonly string _relationIdField;
      private readonly string _relationTypeField;
      private readonly string _projObjIdField;
      private readonly string _partObjIdField;
      private readonly string _projObjTypeField;
      private readonly string _partObjTypeField;
      private readonly int _relationIdFieldIndex;
      private readonly int _relationTypeFieldIndex;
      private readonly int _projObjIdFieldIndex;
      private readonly int _partObjIdFieldIndex;
      private readonly int _projObjTypeFieldIndex;
      private readonly int _partObjTypeFieldIndex;
      private readonly bool _columnIndexMode;
      /// <summary>
      /// 
      /// </summary>
      private static IEnumerable<ColumnDescriptor> _columns;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataRow"></param>
      /// <param name="objectIdField"></param>
      /// <param name="objectTypeField"></param>
      /// <returns></returns>
      private T ParseObjectItem(DataRow dataRow, string objectIdField, string objectTypeField)
      {
        T obj = new T();
        obj.ObjectID = DataSetProcessor.GetInt64Value(dataRow, objectIdField, 0L);
        T objectItem = obj;
        if (!string.IsNullOrEmpty(objectTypeField))
          objectItem.ObjTypeID = DataSetProcessor.GetInt32Value(dataRow, objectTypeField, -1);
        return objectItem;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataRow"></param>
      /// <param name="objectIdFieldIndex"></param>
      /// <param name="objectTypeFieldIndex"></param>
      /// <returns></returns>
      private T ParseObjectItem(DataRow dataRow, int objectIdFieldIndex, int objectTypeFieldIndex)
      {
        T obj = new T();
        obj.ObjectID = DataSetProcessor.GetInt64Value(dataRow, objectIdFieldIndex, 0L);
        T objectItem = obj;
        if (objectTypeFieldIndex != -1)
          objectItem.ObjTypeID = DataSetProcessor.GetInt32Value(dataRow, objectTypeFieldIndex, -1);
        return objectItem;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="relationIdField"></param>
      /// <param name="relationTypeField"></param>
      /// <param name="projObjIdField"></param>
      /// <param name="partObjIdField"></param>
      /// <param name="projObjTypeField"></param>
      /// <param name="partObjTypeField"></param>
      public RelObjInfoDbScheme(
        string relationIdField = "F_PRJLINK_ID",
        string relationTypeField = "F_RELATION_TYPE",
        string projObjIdField = "F_PROJ_ID",
        string partObjIdField = "F_OBJECT_ID",
        string projObjTypeField = "",
        string partObjTypeField = "F_OBJECT_TYPE")
      {
        this._relationIdField = relationIdField;
        this._relationTypeField = relationTypeField;
        this._projObjIdField = projObjIdField;
        this._partObjIdField = partObjIdField;
        this._projObjTypeField = projObjTypeField;
        this._partObjTypeField = partObjTypeField;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="relationIdField"></param>
      /// <param name="relationTypeField"></param>
      /// <param name="projObjIdField"></param>
      /// <param name="partObjIdField"></param>
      /// <param name="projObjTypeField"></param>
      /// <param name="partObjTypeField"></param>
      public RelObjInfoDbScheme(bool isComposition)
        : this(partObjIdField: isComposition ? "F_OBJECT_ID" : RelObjInfoDbScheme<T>.Consts.PartObjectId, projObjTypeField: isComposition ? string.Empty : "F_OBJECT_TYPE", partObjTypeField: isComposition ? "F_OBJECT_TYPE" : string.Empty)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="relationIdField"></param>
      /// <param name="relationTypeField"></param>
      /// <param name="projObjIdField"></param>
      /// <param name="partObjIdField"></param>
      /// <param name="projObjTypeField"></param>
      /// <param name="partObjTypeField"></param>
      public RelObjInfoDbScheme(
        int relationIdFieldIndex,
        int relationTypeFieldIndex,
        int projObjIdFieldIndex,
        int partObjIdFieldIndex,
        int projObjTypeFieldIndex = -1,
        int partObjTypeFieldIndex = -1)
      {
        if (relationIdFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (relationIdFieldIndex));
        if (relationTypeFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (relationTypeFieldIndex));
        if (projObjIdFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (projObjIdFieldIndex));
        if (partObjIdFieldIndex < 0)
          throw new ArgumentException("Column's index is undefined", nameof (partObjIdFieldIndex));
        this._relationIdFieldIndex = relationIdFieldIndex;
        this._relationTypeFieldIndex = relationTypeFieldIndex;
        this._projObjIdFieldIndex = projObjIdFieldIndex;
        this._partObjIdFieldIndex = partObjIdFieldIndex;
        this._projObjTypeFieldIndex = projObjTypeFieldIndex;
        this._partObjTypeFieldIndex = partObjTypeFieldIndex;
        this._columnIndexMode = true;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataRow"></param>
      /// <returns></returns>
      public override RelObjInfoItem ParseItem(DataRow dataRow)
      {
        RelObjInfoItem relObjInfoItem = this._columnIndexMode ? new RelObjInfoItem(DataSetProcessor.GetInt64Value(dataRow, this._relationIdFieldIndex, 0L), DataSetProcessor.GetInt32Value(dataRow, this._relationTypeFieldIndex, -1)) : new RelObjInfoItem(DataSetProcessor.GetInt64Value(dataRow, this._relationIdField, 0L), DataSetProcessor.GetInt32Value(dataRow, this._relationTypeField, -1));
        if (this._columnIndexMode)
        {
          relObjInfoItem.ProjInfo = (ObjInfoItem) this.ParseObjectItem(dataRow, this._projObjIdFieldIndex, this._projObjTypeFieldIndex);
          relObjInfoItem.PartInfo = (ObjInfoItem) this.ParseObjectItem(dataRow, this._partObjIdFieldIndex, this._partObjTypeFieldIndex);
        }
        else
        {
          relObjInfoItem.ProjInfo = (ObjInfoItem) this.ParseObjectItem(dataRow, this._projObjIdField, this._projObjTypeField);
          relObjInfoItem.PartInfo = (ObjInfoItem) this.ParseObjectItem(dataRow, this._partObjIdField, this._partObjTypeField);
        }
        return relObjInfoItem;
      }

      /// <summary>
      /// Получение списка полей таблицы, для заполнения объектов
      /// </summary>
      /// <returns></returns>
      public static IEnumerable<ColumnDescriptor> GetSourceTableColumns()
      {
        if (RelObjInfoDbScheme<T>._columns == null)
          RelObjInfoDbScheme<T>._columns = (IEnumerable<ColumnDescriptor>) new List<ColumnDescriptor>()
          {
            new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -22, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -7, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
          };
        return (IEnumerable<ColumnDescriptor>) RelObjInfoDbScheme<T>._columns.ToArray<ColumnDescriptor>();
      }

      public bool ParseInfoItems(
        [NotNull] IUserSession session,
        [NotNull] IEnumerable<DataRow> dataRows,
        ICollection<RelObjInfoItem> objects)
      {
        if (objects == null || !this.ParseItems(dataRows, objects))
          return false;
        Dictionary<long, ObjInfoItem> dictionary = new Dictionary<long, ObjInfoItem>();
        List<ObjInfoItem> objInfoList = new List<ObjInfoItem>();
        foreach (RelObjInfoItem relObjInfoItem in (IEnumerable<RelObjInfoItem>) objects)
        {
          if (relObjInfoItem.PartInfo.HasEmptyInfo)
            objInfoList.Add(relObjInfoItem.PartInfo);
          else
            dictionary[relObjInfoItem.PartInfo.ObjectID] = relObjInfoItem.PartInfo;
          if (relObjInfoItem.ProjInfo.HasEmptyInfo)
            objInfoList.Add(relObjInfoItem.ProjInfo);
          else
            dictionary[relObjInfoItem.ProjInfo.ObjectID] = relObjInfoItem.ProjInfo;
        }
        for (int index = objInfoList.Count - 1; index >= 0; --index)
        {
          ObjInfoItem objInfoItem1 = objInfoList[index];
          ObjInfoItem objInfoItem2;
          if (dictionary.TryGetValue(objInfoItem1.ObjectID, out objInfoItem2))
          {
            objInfoItem1.CopyFrom((TypedInfoItem) objInfoItem2);
            objInfoList.RemoveAt(index);
          }
        }
        if (objInfoList.Count != 0)
          ObjInfoHelper.UpdateUnknownInfo((IEnumerable<ObjInfoItem>) objInfoList, session);
        return true;
      }

      /// <summary>
      /// 
      /// </summary>
      public static class Consts
      {
        /// <summary>
        /// F_PART_OBJ_ID (Поле с ид. версии дочернего узла при поиске применяемости объекта)
        /// </summary>
        public static string PartObjectId = "F_PART_OBJ_ID";
      }
    }
}
