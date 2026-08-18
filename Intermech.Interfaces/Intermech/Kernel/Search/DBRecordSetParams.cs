
// Type: Intermech.Kernel.Search.DBRecordSetParams
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Структура свойств, описывающих выборку записей из базы данных, которую нужно передать на клиента
    /// </summary>
    [Serializable]
    public struct DBRecordSetParams
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
      /// <summary>
      /// Дополнительные данные, которые могут передаваться на серверную сторону со стороны клиентских плагинов.
      /// Для заполнения данного контейнера данными необходимо воспользоваться услугами службы IClientPluginsService
      /// </summary>
      public HybridDictionary Tags;

      public DBRecordSetParams(
        ConditionStructure[] conditions,
        object[] columns,
        ColumnInfo[] columnsInfo,
        object[] sortColumns,
        SortOrders[] orders,
        long lastKeyValue,
        object lastOrderValue,
        int recordCount,
        bool failIfNotFound,
        string tableName)
      {
        if (sortColumns != null && orders != null && sortColumns.Length != orders.Length)
          throw new Exception("Error: sortColumns.Length <> orders.Length");
        if (columnsInfo != null && columns != null && columns.Length != columnsInfo.Length)
          throw new Exception("Error: columns.Length <> columnsInfo.Length");
        this.SortSources = (AttributeSourceTypes[]) null;
        this.Conditions = conditions;
        this.Columns = columns;
        this.SortColumns = sortColumns;
        this.Orders = orders;
        this.LastKeyValue = lastKeyValue;
        this.LastOrderValue = lastOrderValue;
        this.RecordCount = recordCount;
        this.FailIfNotFound = failIfNotFound;
        this.TableName = tableName;
        this.ColumnsInfo = columnsInfo;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
      }

      public DBRecordSetParams(
        ConditionStructure[] conditions,
        object[] columns,
        object[] sortColumns,
        SortOrders[] orders,
        long lastKeyValue,
        object lastOrderValue,
        int recordCount,
        bool failIfNotFound,
        string tableName)
      {
        if (sortColumns != null && orders != null && sortColumns.Length != orders.Length)
          throw new KernelException("Error: sortColumns.Length <> orders.Length");
        this.SortSources = (AttributeSourceTypes[]) null;
        this.Conditions = conditions;
        this.Columns = columns;
        this.SortColumns = sortColumns;
        this.Orders = orders;
        this.LastKeyValue = lastKeyValue;
        this.LastOrderValue = lastOrderValue;
        this.RecordCount = recordCount;
        this.FailIfNotFound = failIfNotFound;
        this.TableName = tableName;
        this.ColumnsInfo = (ColumnInfo[]) null;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
      }

      public DBRecordSetParams(
        ConditionStructure[] conditions,
        object[] columns,
        long lastKeyValue,
        object lastOrderValue,
        int recordCount)
      {
        this.Conditions = conditions;
        this.Columns = columns;
        this.SortColumns = (object[]) null;
        this.Orders = (SortOrders[]) null;
        this.LastKeyValue = lastKeyValue;
        this.LastOrderValue = lastOrderValue;
        this.RecordCount = recordCount;
        this.FailIfNotFound = true;
        this.TableName = string.Empty;
        this.ColumnsInfo = (ColumnInfo[]) null;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortSources = (AttributeSourceTypes[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
      }

      public DBRecordSetParams(
        ConditionStructure[] conditions,
        object[] columns,
        object[] sortColumns,
        SortOrders[] orders)
      {
        if (sortColumns != null && orders != null && sortColumns.Length != orders.Length)
          throw new KernelException("Error: sortColumns.Length <> orders.Length");
        this.SortSources = (AttributeSourceTypes[]) null;
        this.Conditions = conditions;
        this.Columns = columns;
        this.SortColumns = sortColumns;
        this.Orders = orders;
        this.LastKeyValue = 0L;
        this.LastOrderValue = (object) null;
        this.RecordCount = -1;
        this.FailIfNotFound = true;
        this.TableName = string.Empty;
        this.ColumnsInfo = (ColumnInfo[]) null;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
      }

      public DBRecordSetParams(ConditionStructure[] conditions, object[] columns)
      {
        this.Conditions = conditions;
        this.Columns = columns;
        this.SortColumns = (object[]) null;
        this.Orders = (SortOrders[]) null;
        this.LastKeyValue = 0L;
        this.LastOrderValue = (object) null;
        this.RecordCount = -1;
        this.FailIfNotFound = true;
        this.TableName = string.Empty;
        this.ColumnsInfo = (ColumnInfo[]) null;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortSources = (AttributeSourceTypes[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
      }

      public DBRecordSetParams(ConditionStructure[] conditions)
      {
        this.Conditions = conditions;
        this.Columns = (object[]) null;
        this.SortColumns = (object[]) null;
        this.Orders = (SortOrders[]) null;
        this.LastKeyValue = 0L;
        this.LastOrderValue = (object) null;
        this.RecordCount = -1;
        this.FailIfNotFound = true;
        this.TableName = string.Empty;
        this.ColumnsInfo = (ColumnInfo[]) null;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortSources = (AttributeSourceTypes[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
      }

      public DBRecordSetParams(int recordCount)
      {
        this.Conditions = (ConditionStructure[]) null;
        this.Columns = (object[]) null;
        this.SortColumns = (object[]) null;
        this.Orders = (SortOrders[]) null;
        this.LastKeyValue = 0L;
        this.LastOrderValue = (object) null;
        this.RecordCount = recordCount;
        this.FailIfNotFound = true;
        this.TableName = string.Empty;
        this.ColumnsInfo = (ColumnInfo[]) null;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortSources = (AttributeSourceTypes[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
      }

      public void SetColumnDescriptors(ColumnDescriptor[] columns)
      {
        if (columns == null)
          return;
        this.Columns = new object[columns.Length];
        ArrayList arrayList = new ArrayList();
        bool flag1 = false;
        bool flag2 = false;
        for (int index = 0; index < this.Columns.Length; ++index)
        {
          this.Columns[index] = columns[index].AttributeID;
          if (columns[index].Sort != SortOrders.NONE)
            arrayList.Add((object) index);
          if (columns[index].ColumnName != ColumnNameMapping.Default)
            flag1 = true;
          if (columns[index].Contents != ColumnContents.Text)
            flag2 = true;
        }
        if (arrayList.Count > 0)
        {
          this.SortColumns = new object[arrayList.Count];
          this.Orders = new SortOrders[arrayList.Count];
          this.SortSources = new AttributeSourceTypes[arrayList.Count];
          this.SortContents = new ColumnContents[arrayList.Count];
          int index1 = 0;
          while (arrayList.Count > 0)
          {
            int num = int.MaxValue;
            int index2 = -1;
            for (int index3 = 0; index3 < arrayList.Count; ++index3)
            {
              int index4 = (int) arrayList[index3];
              if (columns[index4].OrderByID < num)
              {
                index2 = index3;
                num = columns[index4].OrderByID;
              }
            }
            this.SortColumns[index1] = columns[(int) arrayList[index2]].AttributeID;
            this.SortSources[index1] = columns[(int) arrayList[index2]].AttributeSource;
            this.SortContents[index1] = columns[(int) arrayList[index2]].Contents;
            this.Orders[index1++] = columns[(int) arrayList[index2]].Sort;
            arrayList.RemoveAt(index2);
          }
        }
        else
        {
          this.SortColumns = (object[]) null;
          this.Orders = (SortOrders[]) null;
        }
        this.ColumnsInfo = new ColumnInfo[columns.Length];
        for (int index = 0; index < columns.Length; ++index)
          this.ColumnsInfo[index] = new ColumnInfo(columns[index].AttributeID, columns[index].AttributeSource, (object) null);
        if (flag2)
        {
          this.Contents = new ColumnContents[columns.Length];
          for (int index = 0; index < columns.Length; ++index)
            this.Contents[index] = columns[index].Contents;
        }
        else
          this.Contents = (ColumnContents[]) null;
        if (flag1)
        {
          this.ColumnNames = new ColumnNameMapping[columns.Length];
          for (int index = 0; index < columns.Length; ++index)
            this.ColumnNames[index] = columns[index].ColumnName;
        }
        else
          this.ColumnNames = (ColumnNameMapping[]) null;
      }

      /// <summary>Добавить дополнительные столбцы к параметрам запроса</summary>
      /// <param name="AddColumns">Дополнительные столбцы</param>
      /// <param name="AddedColumnsPos">Если данный параметр задан, то в нём будет возвращён список позиций добавленных колонок</param>
      /// <returns>Вернёт количество реально добавленных столбцов</returns>
      public int AddColumnDescriptors(ColumnDescriptor[] AddColumns, List<int> AddedColumnsPos)
      {
        AddedColumnsPos?.Clear();
        if (AddColumns == null || AddColumns.Length == 0)
          return 0;
        if (this.Columns == null)
          this.Columns = new object[0];
        if (this.Columns.Length != 0 && (this.ColumnNames == null || this.ColumnNames.Length != this.Columns.Length))
        {
          ColumnNameMapping[] columnNameMappingArray = new ColumnNameMapping[this.Columns.Length];
          for (int index = 0; index < columnNameMappingArray.Length; ++index)
            columnNameMappingArray[index] = ColumnNameMapping.Default;
          if (this.ColumnNames != null && this.ColumnNames.Length != 0)
          {
            for (int index = 0; index < this.ColumnNames.Length; ++index)
              columnNameMappingArray[index] = this.ColumnNames[index];
          }
          this.ColumnNames = columnNameMappingArray;
        }
        if (this.Columns.Length != 0 && (this.ColumnsInfo == null || this.ColumnsInfo.Length != this.Columns.Length))
        {
          ColumnInfo[] columnInfoArray = new ColumnInfo[this.Columns.Length];
          for (int index = 0; index < columnInfoArray.Length; ++index)
            columnInfoArray[index] = new ColumnInfo(this.Columns[index], AttributeSourceTypes.Auto, (object) null);
          if (this.ColumnsInfo != null && this.ColumnsInfo.Length != 0)
          {
            for (int index = 0; index < this.ColumnsInfo.Length; ++index)
              columnInfoArray[index] = this.ColumnsInfo[index];
          }
          this.ColumnsInfo = columnInfoArray;
        }
        ArrayList arrayList1 = new ArrayList(this.Columns.Length);
        ArrayList arrayList2 = new ArrayList(this.Columns.Length);
        for (int index = 0; index < this.Columns.Length; ++index)
        {
          if (this.Columns[index].GetType() == typeof (ObligatoryObjectAttributes))
            arrayList1.Add((object) Convert.ToInt32(this.Columns[index]));
          else
            arrayList1.Add(this.Columns[index]);
          arrayList2.Add((object) this.ColumnNames[index]);
        }
        ArrayList arrayList3 = new ArrayList();
        for (int index1 = 0; index1 < AddColumns.Length; ++index1)
        {
          ColumnDescriptor addColumn1 = AddColumns[index1];
          object attributeId = AddColumns[index1].AttributeID;
          if (AddColumns[index1].AttributeID.GetType() == typeof (ObligatoryObjectAttributes) || AddColumns[index1].AttributeID.GetType() == typeof (int))
            attributeId = (object) (int) AddColumns[index1].AttributeID;
          bool flag = false;
          for (int index2 = 0; index2 < arrayList1.Count; ++index2)
          {
            flag = (ColumnNameMapping) arrayList2[index2] == addColumn1.ColumnName && arrayList1[index2].Equals(attributeId);
            if (flag)
              break;
          }
          if (!flag)
          {
            ColumnDescriptor addColumn2 = AddColumns[index1];
            arrayList3.Add((object) addColumn2);
          }
        }
        if (arrayList3.Count <= 0)
          return 0;
        object[] objArray = new object[this.Columns.Length + arrayList3.Count];
        for (int index = 0; index < this.Columns.Length; ++index)
          objArray[index] = this.Columns[index];
        int length = this.Columns.Length;
        ArrayList arrayList4 = new ArrayList();
        if (arrayList3.Count > 0)
        {
          for (int index = 0; index < arrayList3.Count; ++index)
          {
            ColumnDescriptor columnDescriptor = (ColumnDescriptor) arrayList3[index];
            objArray[index + length] = columnDescriptor.AttributeID;
            AddedColumnsPos?.Add(index + length);
          }
        }
        this.Columns = objArray;
        int num1 = 0;
        if (this.ColumnsInfo != null)
          num1 = this.ColumnsInfo.Length;
        ColumnInfo[] columnInfoArray1 = new ColumnInfo[this.Columns.Length];
        for (int index = 0; index < num1; ++index)
          columnInfoArray1[index] = this.ColumnsInfo[index];
        for (int index = num1; index < arrayList3.Count + num1; ++index)
        {
          ColumnDescriptor columnDescriptor = (ColumnDescriptor) arrayList3[index - num1];
          columnInfoArray1[index] = new ColumnInfo(columnDescriptor.AttributeID, columnDescriptor.AttributeSource, (object) null);
        }
        this.ColumnsInfo = columnInfoArray1;
        int num2 = this.Columns.Length - arrayList3.Count;
        ColumnContents[] columnContentsArray = new ColumnContents[this.Columns.Length];
        for (int index = 0; index < num2; ++index)
          columnContentsArray[index] = ColumnContents.Text;
        if (this.Contents != null)
        {
          for (int index = 0; index < this.Contents.Length; ++index)
            columnContentsArray[index] = this.Contents[index];
        }
        for (int index = num2; index < arrayList3.Count + num2; ++index)
        {
          ColumnDescriptor columnDescriptor = (ColumnDescriptor) arrayList3[index - num2];
          columnContentsArray[index] = columnDescriptor.Contents;
        }
        this.Contents = columnContentsArray;
        int num3 = this.Columns.Length - arrayList3.Count;
        ColumnNameMapping[] columnNameMappingArray1 = new ColumnNameMapping[this.Columns.Length];
        for (int index = 0; index < num3; ++index)
          columnNameMappingArray1[index] = ColumnNameMapping.Default;
        if (this.ColumnNames != null)
        {
          for (int index = 0; index < this.ColumnNames.Length; ++index)
            columnNameMappingArray1[index] = this.ColumnNames[index];
        }
        for (int index = num3; index < arrayList3.Count + num3; ++index)
        {
          ColumnDescriptor columnDescriptor = (ColumnDescriptor) arrayList3[index - num3];
          columnNameMappingArray1[index] = columnDescriptor.ColumnName;
        }
        this.ColumnNames = columnNameMappingArray1;
        if (this.Columns.Length != this.ColumnNames.Length || this.Columns.Length != this.Contents.Length || this.Columns.Length != this.ColumnsInfo.Length)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Interfaces_517"), (object) this.Columns.Length, (object) this.ColumnsInfo.Length, (object) this.Contents.Length));
        return arrayList3.Count;
      }

      /// <summary>
      /// ЕДИНСТВЕННЫЙ конструктор, принимающий массив ColumnDescriptor'ов!
      /// </summary>
      /// <param name="conditions">Массив условий (ConditionStructure)</param>
      /// <param name="columns">Массив ColumnDescriptor'ов, описывающих столбцы</param>
      /// <param name="lastKeyValue">Можно передать 0</param>
      /// <param name="lastOrderValue">Можно передать null</param>
      /// <param name="recordCount">Можно передать QueryConsts.All</param>
      public DBRecordSetParams(
        ConditionStructure[] conditions,
        ColumnDescriptor[] columns,
        long lastKeyValue = 0,
        object lastOrderValue = null,
        int recordCount = -1)
      {
        this.SortSources = (AttributeSourceTypes[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Conditions = conditions;
        this.LastKeyValue = lastKeyValue;
        this.LastOrderValue = lastOrderValue;
        this.RecordCount = recordCount;
        this.FailIfNotFound = true;
        this.TableName = string.Empty;
        this.Columns = (object[]) null;
        this.SortColumns = (object[]) null;
        this.Orders = (SortOrders[]) null;
        this.ColumnsInfo = (ColumnInfo[]) null;
        this.Contents = (ColumnContents[]) null;
        this.ColumnNames = (ColumnNameMapping[]) null;
        this.SortSources = (AttributeSourceTypes[]) null;
        this.SortContents = (ColumnContents[]) null;
        this.Tags = (HybridDictionary) null;
        this.SetColumnDescriptors(columns);
      }

      /// <summary>
      /// Внимание! Конструктор не копирует массивы, а запоминает ссылки на массивы в исходном dbParams! Юзать осторожно!
      /// </summary>
      /// <param name="dbParams">Исходный набор параметров запроса.</param>
      public DBRecordSetParams(DBRecordSetParams dbParams)
      {
        this.Conditions = dbParams.Conditions;
        this.Columns = dbParams.Columns;
        this.SortColumns = dbParams.SortColumns;
        this.Orders = dbParams.Orders;
        this.LastKeyValue = dbParams.LastKeyValue;
        this.LastOrderValue = dbParams.LastOrderValue;
        this.RecordCount = dbParams.RecordCount;
        this.FailIfNotFound = dbParams.FailIfNotFound;
        this.TableName = dbParams.TableName;
        this.ColumnsInfo = dbParams.ColumnsInfo;
        this.Contents = dbParams.Contents;
        this.ColumnNames = dbParams.ColumnNames;
        this.SortSources = dbParams.SortSources;
        this.SortContents = dbParams.SortContents;
        this.Tags = dbParams.Tags;
      }

      /// <summary>
      /// Найти в указанной таблице столбец с атрибутом по имени атрибута, его идентификатору или его Guid
      /// </summary>
      /// <param name="source">Таблица, в колонках которой разыскивается атрибут</param>
      /// <param name="attribute">int, string, guid - идентификатор, название или guid разыскиваемого атрибута</param>
      /// <returns>-1, если атрибут не найден, иначе порядковый номер колонки атрибута в таблице</returns>
      public static int IndexOf(DataTable source, object attribute)
      {
        int num1 = -1;
        if (source == null || attribute == null)
          return num1;
        Type type = attribute.GetType();
        if (type == typeof (int))
        {
          int num2 = (int) attribute;
          return source.Columns.IndexOf(num2.ToString());
        }
        if (type == typeof (ObligatoryObjectAttributes))
        {
          ObligatoryObjectAttributes attr = (ObligatoryObjectAttributes) attribute;
          int num3 = source.Columns.IndexOf(attr.ToString());
          if (num3 >= 0)
            return num3;
          string caption = ObligatoryObjectAttributesHelper.GetCaption(attr);
          return source.Columns.IndexOf(caption);
        }
        if (type == typeof (string))
        {
          string columnName = (string) attribute;
          return source.Columns.IndexOf(columnName);
        }
        if (!(type == typeof (Guid)))
          return num1;
        string columnName1 = ((Guid) attribute).ToString();
        return source.Columns.IndexOf(columnName1);
      }

      /// <summary>
      /// Возвращает копию параметров (чтобы ее можно было портить, не опасаясь загубить исходные параметры)
      /// </summary>
      /// <returns>Копия параметров запроса (за исключением Tags)</returns>
      public DBRecordSetParams Clone()
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
        if (this.SortSources != null)
        {
          dbRecordSetParams.SortSources = new AttributeSourceTypes[this.SortSources.Length];
          for (int index = 0; index < this.SortSources.Length; ++index)
            dbRecordSetParams.SortSources[index] = this.SortSources[index];
        }
        if (this.Conditions != null)
        {
          dbRecordSetParams.Conditions = new ConditionStructure[this.Conditions.Length];
          for (int index = 0; index < this.Conditions.Length; ++index)
            dbRecordSetParams.Conditions[index] = this.Conditions[index].Clone();
        }
        if (this.Columns != null)
        {
          dbRecordSetParams.Columns = new object[this.Columns.Length];
          for (int index = 0; index < this.Columns.Length; ++index)
            dbRecordSetParams.Columns[index] = this.Columns[index];
        }
        if (this.SortColumns != null)
        {
          dbRecordSetParams.SortColumns = new object[this.SortColumns.Length];
          for (int index = 0; index < this.SortColumns.Length; ++index)
            dbRecordSetParams.SortColumns[index] = this.SortColumns[index];
        }
        if (this.Orders != null)
        {
          dbRecordSetParams.Orders = new SortOrders[this.Orders.Length];
          for (int index = 0; index < this.Orders.Length; ++index)
            dbRecordSetParams.Orders[index] = this.Orders[index];
        }
        dbRecordSetParams.LastKeyValue = this.LastKeyValue;
        dbRecordSetParams.LastOrderValue = this.LastOrderValue;
        dbRecordSetParams.RecordCount = this.RecordCount;
        dbRecordSetParams.FailIfNotFound = this.FailIfNotFound;
        dbRecordSetParams.TableName = this.TableName;
        if (this.ColumnsInfo != null)
        {
          dbRecordSetParams.ColumnsInfo = new ColumnInfo[this.ColumnsInfo.Length];
          for (int index = 0; index < this.ColumnsInfo.Length; ++index)
            dbRecordSetParams.ColumnsInfo[index] = this.ColumnsInfo[index];
        }
        if (this.Contents != null)
        {
          dbRecordSetParams.Contents = new ColumnContents[this.Contents.Length];
          for (int index = 0; index < this.Contents.Length; ++index)
            dbRecordSetParams.Contents[index] = this.Contents[index];
        }
        if (this.ColumnNames != null)
        {
          dbRecordSetParams.ColumnNames = new ColumnNameMapping[this.ColumnNames.Length];
          for (int index = 0; index < this.ColumnNames.Length; ++index)
            dbRecordSetParams.ColumnNames[index] = this.ColumnNames[index];
        }
        if (this.SortContents != null)
        {
          dbRecordSetParams.SortContents = new ColumnContents[this.SortContents.Length];
          for (int index = 0; index < this.SortContents.Length; ++index)
            dbRecordSetParams.SortContents[index] = this.SortContents[index];
        }
        dbRecordSetParams.Tags = this.Tags;
        return dbRecordSetParams;
      }

      /// <summary>Добавляет ключ/значение в Tags</summary>
      /// <param name="key">Ключ</param>
      /// <param name="val">Значение</param>
      public void AddTag(object key, object val)
      {
        if (this.Tags == null)
          this.Tags = new HybridDictionary();
        this.Tags.Add(key, val);
      }
    }
}
