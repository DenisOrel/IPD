
// Type: Intermech.Interfaces.WebPortal.DBQueryParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class DBQueryParams
    {
      /// <summary>Условия, по которым должен вестись поиск</summary>
      public ConditionStructure[] Conditions;
      /// <summary>
      /// Массив с идентификаторами (число, guid или имя) колонок, которые должны быть включены в выборку
      /// </summary>
      public object[] Columns;
      /// <summary>
      /// Доп. инфа по колонкам (заполняется тока в случае необходимости)
      /// </summary>
      public ColumnInfo[] ColumnsInfo;
      /// <summary>
      /// Массив с идентификаторами колонок, по которым нужно отсортировать выборку объектов
      /// </summary>
      public object[] SortColumns;
      /// <summary>
      /// Порядок сортировки значений (если список пустой, то принимается сортировка по возрастанию)
      /// </summary>
      public SortOrders[] Orders;
      /// <summary>
      /// Источник атрибутов, по которым идет сортировка. Должен быть в Columns!
      /// </summary>
      public AttributeSourceTypes[] SortSources;
      /// <summary>
      /// Вид информации в атрибуте, по которому идет сортировка. Должен быть в Columns!
      /// </summary>
      public ColumnContents[] SortContents;
      /// <summary>
      /// Последнее значение ключевого поля, которое было передано клиенту (по умолчанию=0)
      /// </summary>
      public long LastKeyValue;
      /// <summary>
      /// Последнее значение первого поля сортировки, которое было передано клиенту (по умолчанию=null)
      /// </summary>
      public object LastOrderValue;
      /// <summary>
      /// Количество записей, которые нужно передать клиенту (по умолчанию=QueryConsts.Default)
      /// </summary>
      public int RecordCount;
      /// <summary>
      /// Генерировать ли исключение AttributeTypeNotFoundException в случае, если какой-либо атрибут
      /// в списках Columns и SortColumns отсутствует. Если false - атрибут просто не будет
      /// включен в результирующую таблицу. По умолчанию true.
      /// </summary>
      public bool FailIfNotFound;
      /// <summary>Имя таблицы, которую вернет ф-ция SELECT</summary>
      public string TableName;
      /// <summary>
      /// Виды информации, которую нужно получить по атрибутам из Columns.
      /// </summary>
      public ColumnContents[] Contents;
      /// <summary>Способы именования колонок атрибутов Columns</summary>
      public ColumnNameMapping[] ColumnNames;

      public static DBQueryParams FormingParams(DBRecordSetParams dbParams)
      {
        DBQueryParams dbQueryParams = new DBQueryParams();
        dbQueryParams.SortSources = dbParams.SortSources;
        dbQueryParams.Conditions = dbParams.Conditions;
        dbQueryParams.Columns = dbParams.Columns;
        dbQueryParams.SortColumns = dbParams.SortColumns;
        dbQueryParams.Orders = dbParams.Orders;
        dbQueryParams.LastKeyValue = dbParams.LastKeyValue;
        if (dbParams.LastOrderValue != null && dbParams.LastOrderValue is List<object>)
        {
          List<object> lastOrderValue = (List<object>) dbParams.LastOrderValue;
          dbQueryParams.LastOrderValue = (object) new object[lastOrderValue.Count];
          for (int index = 0; index < lastOrderValue.Count; ++index)
            ((object[]) dbQueryParams.LastOrderValue)[index] = lastOrderValue[index];
        }
        dbQueryParams.RecordCount = dbParams.RecordCount;
        dbQueryParams.FailIfNotFound = dbParams.FailIfNotFound;
        dbQueryParams.TableName = dbParams.TableName;
        dbQueryParams.ColumnsInfo = dbParams.ColumnsInfo;
        dbQueryParams.Contents = dbParams.Contents;
        dbQueryParams.ColumnNames = dbParams.ColumnNames;
        dbQueryParams.SortContents = dbParams.SortContents;
        return dbQueryParams;
      }

      public static DBRecordSetParams UnformingParams(DBQueryParams queryParams)
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
        dbRecordSetParams.SortSources = queryParams.SortSources;
        dbRecordSetParams.Conditions = queryParams.Conditions;
        dbRecordSetParams.Columns = queryParams.Columns;
        dbRecordSetParams.SortColumns = queryParams.SortColumns;
        dbRecordSetParams.Orders = queryParams.Orders;
        dbRecordSetParams.LastKeyValue = queryParams.LastKeyValue;
        if (queryParams.LastOrderValue != null && queryParams.LastOrderValue is object[])
          dbRecordSetParams.LastOrderValue = (object) new List<object>((IEnumerable<object>) (object[]) queryParams.LastOrderValue);
        dbRecordSetParams.RecordCount = queryParams.RecordCount;
        dbRecordSetParams.FailIfNotFound = queryParams.FailIfNotFound;
        dbRecordSetParams.TableName = queryParams.TableName;
        dbRecordSetParams.ColumnsInfo = queryParams.ColumnsInfo;
        dbRecordSetParams.Contents = queryParams.Contents;
        dbRecordSetParams.ColumnNames = queryParams.ColumnNames;
        dbRecordSetParams.SortContents = queryParams.SortContents;
        return dbRecordSetParams;
      }
    }
}
