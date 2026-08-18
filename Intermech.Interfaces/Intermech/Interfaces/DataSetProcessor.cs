
// Type: Intermech.Interfaces.DataSetProcessor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;


namespace Intermech.Interfaces
{
    /// <summary>Класс для различных внутрисистемных обработок Dataset</summary>
    public class DataSetProcessor
    {
      public static readonly string[] VarcharFields = new string[9]
      {
        "F_STRING_VALUE",
        "F_NOTE",
        "F_OBJ_NAME",
        "F_FILENAME",
        "F_OBJECT_NAME",
        "F_OBJ_TYPE_NAME",
        "F_AREA_ID",
        "CAPTION",
        "F_COMPUTER_NAME"
      };
      public static Hashtable _captions;

      /// <summary>Заполняет инфу по первичным ключам</summary>
      public static void FillPrimaryKeys(Hashtable ht)
      {
        ht.Add((object) "IMS_ATTR_GROUPS", (object) new string[1]
        {
          "F_GROUP_ID"
        });
        ht.Add((object) "IMS_ATTR4OBJ_TYPES", (object) new string[2]
        {
          "F_ATTRIBUTE_ID",
          "F_OBJECT_TYPE"
        });
        ht.Add((object) "IMS_ATTR4RELATION_TYPES", (object) new string[2]
        {
          "F_RELATION_TYPE",
          "F_ATTRIBUTE_ID"
        });
        ht.Add((object) "IMS_ATTRIBUTES", (object) new string[1]
        {
          "F_ATTRIBUTE_ID"
        });
        ht.Add((object) "IMS_LANGUAGES", (object) new string[1]
        {
          "F_LANGUAGE_ID"
        });
        ht.Add((object) "IMS_LC_STEPS", (object) new string[1]
        {
          "F_LC_STEP"
        });
        ht.Add((object) "IMS_LEVELS", (object) new string[1]
        {
          "F_LEVEL_ID"
        });
        ht.Add((object) "IMS_OBJECT_TYPES", (object) new string[1]
        {
          "F_OBJECT_TYPE"
        });
        ht.Add((object) "IMS_RELATION_TYPES", (object) new string[1]
        {
          "F_RELATION_TYPE"
        });
        ht.Add((object) "IMS_SUBJECT_AREAS", (object) new string[1]
        {
          "F_AREA_ID"
        });
        ht.Add((object) "IMS_METADATA", (object) new string[1]
        {
          "F_TABLE_NAME"
        });
        ht.Add((object) "IMS_LC_LINKS", (object) new string[2]
        {
          "F_FROM_STEP",
          "F_TO_STEP"
        });
        ht.Add((object) "IMS_TYPES_APPLICABILITY", (object) new string[1]
        {
          "F_APPLICABILITY_ID"
        });
        ht.Add((object) "IMS_DBVERSION", (object) new string[1]
        {
          "F_MODULE_NAME"
        });
        ht.Add((object) "IMS_LC_SCHEMAS", (object) new string[1]
        {
          "F_SCHEMA_ID"
        });
      }

      /// <summary>
      /// Cоздать первичные ключи на DataSet с системными метаданными
      /// </summary>
      /// <param name="metadataDataSet">метаданные системы</param>
      public static void CreatePrimaryKeys(DataSet metadataDataSet)
      {
        Hashtable ht = new Hashtable(15);
        DataSetProcessor.FillPrimaryKeys(ht);
        foreach (DictionaryEntry dictionaryEntry in ht)
        {
          DataTable table = metadataDataSet.Tables[(string) dictionaryEntry.Key];
          if (table != null)
          {
            object obj = dictionaryEntry.Value;
            if (obj != null)
            {
              string[] strArray = (string[]) obj;
              DataColumn[] dataColumnArray = new DataColumn[strArray.Length];
              for (int index = 0; index < strArray.Length; ++index)
                dataColumnArray[index] = table.Columns[strArray[index]];
              table.PrimaryKey = dataColumnArray;
            }
          }
        }
      }

      /// <summary>
      /// Копирует данные из строки fromRow в таблицу toTable, вставляя их перед строкой номер pos
      /// </summary>
      /// <param name="toTable"></param>
      /// <param name="fromRow"></param>
      /// <param name="pos"></param>
      /// <param name="acceptChanges"></param>
      public static DataRow AssignRow(DataTable toTable, DataRow fromRow, int pos, bool acceptChanges)
      {
        if (fromRow == null)
          return (DataRow) null;
        DataRow row = toTable.NewRow();
        for (int index = 0; index < toTable.Columns.Count; ++index)
          row[toTable.Columns[index].ColumnName] = fromRow[toTable.Columns[index].ColumnName];
        toTable.Rows.InsertAt(row, pos);
        if (acceptChanges)
          toTable.AcceptChanges();
        return row;
      }

      /// <summary>Добавляет данные из строки fromRow в таблицу toTable</summary>
      /// <remarks>Рекомендуется использовать данный метод только, если схемы (набор полей) у fromRow и toTable отличается.
      /// Если схемы данных совпадает - прямое копирование через DataTable.Rows.Add(Row.ToArray()) работает быстрее </remarks>
      public static DataRow AddRow(DataTable toTable, DataRow fromRow, bool acceptChanges)
      {
        if (fromRow == null)
          return (DataRow) null;
        lock (toTable)
        {
          try
          {
            if (fromRow.Table != null)
            {
              if (fromRow.RowState != DataRowState.Detached)
                toTable.ImportRow(fromRow);
              else if (fromRow.Table == toTable)
              {
                toTable.Rows.Add(fromRow);
              }
              else
              {
                DataRow row = toTable.NewRow();
                for (int index = 0; index < toTable.Columns.Count; ++index)
                  row[toTable.Columns[index].ColumnName] = fromRow[toTable.Columns[index].ColumnName];
                toTable.Rows.Add(row);
                return row;
              }
            }
            else
              toTable.Rows.Add(fromRow);
            return fromRow;
          }
          finally
          {
            if (acceptChanges)
              toTable.AcceptChanges();
          }
        }
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="dataRow"></param>
      /// <param name="buffer"></param>
      /// <param name="columnCount">Кол-во копируемых столбов</param>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void CopyDataToBuffer(DataRow dataRow, object[] buffer, int columnCount = -1)
      {
        columnCount = columnCount >= 0 ? columnCount : dataRow.Table.Columns.Count;
        for (int columnIndex = 0; columnIndex < columnCount; ++columnIndex)
          buffer[columnIndex] = dataRow[columnIndex];
      }

      /// <summary>
      /// Добавляет кавычки к строке и меняет запрещенные кавычки на разрешенные
      /// </summary>
      public static string QString(string aValue)
      {
        if (aValue.IndexOf("'") > -1)
          aValue = aValue.Replace("'", "''");
        return $"'{aValue.Trim()}'";
      }

      /// <summary>Копирует данные из строк fromRows в таблицу toTable</summary>
      /// <param name="fromRows"></param>
      /// <param name="toTable"></param>
      /// <param name="acceptChanges"></param>
      /// <param name="directMode">Режим "прямого" копирования данных без проверки на соответствие столбцов.
      /// Использовать только в случаях когда схемы toTable и всех fromRows совпадают</param>
      public static void AssignRows(
        DataTable toTable,
        IEnumerable<DataRow> fromRows,
        bool acceptChanges,
        bool directMode = false)
      {
        if (fromRows == null)
          return;
        int num = -1;
        if (fromRows is ICollection<DataRow> dataRows)
          num = dataRows.Count;
        else if (fromRows is IList list)
          num = list.Count;
        if (num == 0)
          return;
        toTable.BeginLoadData();
        try
        {
          if (num != -1 && toTable.MinimumCapacity - toTable.Rows.Count < num)
            toTable.MinimumCapacity = toTable.Rows.Count + num;
          int count1 = toTable.Columns.Count;
          object[] buffer = directMode ? new object[count1] : (object[]) null;
          foreach (DataRow fromRow in fromRows)
          {
            int count2 = fromRow.Table.Columns.Count;
            if (directMode && count2 == count1)
            {
              DataSetProcessor.CopyDataToBuffer(fromRow, buffer, count2);
              toTable.Rows.Add(buffer);
            }
            else if (fromRow.RowState != DataRowState.Detached)
              toTable.ImportRow(fromRow);
            else if (fromRow.Table == toTable)
            {
              toTable.Rows.Add(fromRow);
            }
            else
            {
              DataRow row = toTable.NewRow();
              for (int index = 0; index < toTable.Columns.Count; ++index)
                row[toTable.Columns[index].ColumnName] = fromRow[toTable.Columns[index].ColumnName];
              toTable.Rows.Add(row);
            }
          }
        }
        finally
        {
          toTable.EndLoadData();
        }
        if (!acceptChanges)
          return;
        toTable.AcceptChanges();
      }

      /// <summary>
      /// Добавляет содержимое таблицы fromTable в таблицу toTable
      /// </summary>
      /// <param name="toTable">Таблица-назначение</param>
      /// <param name="fromTable">Таблица-источник</param>
      /// <param name="acceptChanges">Внести изменения в таблицу-назначение</param>
      public static void AddTable(DataTable toTable, DataTable fromTable, bool acceptChanges)
      {
        if (toTable == null || fromTable == null)
          return;
        if (toTable.MinimumCapacity - toTable.Rows.Count < fromTable.Rows.Count)
          toTable.MinimumCapacity = toTable.Rows.Count + fromTable.Rows.Count;
        bool flag = true;
        int count = fromTable.Columns.Count;
        if (toTable.Columns.Count != count)
        {
          flag = false;
        }
        else
        {
          int index = 0;
          while (index < toTable.Columns.Count && !(toTable.Columns[index].ColumnName != fromTable.Columns[index].ColumnName) && !(toTable.Columns[index].DataType != fromTable.Columns[index].DataType))
            ++index;
        }
        toTable.BeginLoadData();
        try
        {
          if (flag)
          {
            object[] buffer = new object[count];
            foreach (DataRow row in (InternalDataCollectionBase) fromTable.Rows)
            {
              DataSetProcessor.CopyDataToBuffer(row, buffer, count);
              toTable.Rows.Add(buffer);
            }
          }
          else
          {
            int[] numArray = new int[count];
            for (int index = 0; index < count; ++index)
              numArray[index] = toTable.Columns.IndexOf(fromTable.Columns[index].ColumnName);
            object[] itemArray = toTable.NewRow().ItemArray;
            foreach (DataRow row in (InternalDataCollectionBase) fromTable.Rows)
            {
              for (int columnIndex = 0; columnIndex < count; ++columnIndex)
              {
                int index = numArray[columnIndex];
                if (index != -1)
                  itemArray[index] = row[columnIndex];
              }
              toTable.Rows.Add(itemArray);
            }
          }
        }
        finally
        {
          toTable.EndLoadData();
        }
        if (!acceptChanges)
          return;
        toTable.AcceptChanges();
      }

      /// <summary>Копирует данные из строк fromRows в таблицу toTable</summary>
      public static void AssignRows(DataTable toTable, IEnumerable<DataRow> fromRows)
      {
        DataSetProcessor.AssignRows(toTable, fromRows, true);
      }

      /// <summary>Возвращает копию таблицы</summary>
      public static DataTable CopyTable(DataTable fromTable)
      {
        DataTable dataTable = fromTable.Copy();
        dataTable.RemotingFormat = SerializationFormat.Binary;
        return dataTable;
      }

      public static DataTable FormDataTable(DataRow[] rows)
      {
        if (rows == null || rows.Length == 0)
          return (DataTable) null;
        DataRow row = rows[0];
        DataTable toTable = new DataTable(row.Table.TableName);
        foreach (DataColumn column1 in (InternalDataCollectionBase) row.Table.Columns)
        {
          DataColumn column2 = new DataColumn(column1.ColumnName, column1.DataType);
          toTable.Columns.Add(column2);
        }
        DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) rows);
        return toTable;
      }

      /// <summary>Возвращает true если</summary>
      public static bool IsVarcharField(string fldName)
      {
        return Array.IndexOf<string>(DataSetProcessor.VarcharFields, fldName) >= 0;
      }

      /// <summary>
      /// Изменяет значение поля fieldName таблицы tableName у записи с условием
      /// filterStr на значение newValue
      /// </summary>
      public static void ChangeTableValue(
        string filterStr,
        DataTable toTable,
        string fieldName,
        object newValue)
      {
        lock (toTable)
        {
          DataRow[] dataRowArray = toTable.Select(filterStr);
          if (dataRowArray.Length == 0)
            return;
          foreach (DataRow dataRow in dataRowArray)
            dataRow[fieldName] = newValue;
          toTable.AcceptChanges();
        }
      }

      /// <summary>
      /// Удаляет записи из таблицы tableName по условию condition
      /// </summary>
      /// <param name="toTable"></param>
      /// <param name="condition"></param>
      /// <returns>Возвращает количество удаленных записей</returns>
      public static int DeleteRecords(DataTable toTable, string condition)
      {
        int num = 0;
        lock (toTable)
        {
          foreach (DataRow row in toTable.Select(condition))
          {
            toTable.Rows.Remove(row);
            ++num;
          }
        }
        return num;
      }

      public static void FillCaptions(DataTable datatable)
      {
        foreach (DataColumn column in (InternalDataCollectionBase) datatable.Columns)
          column.Caption = DataSetProcessor.GetCaption(column.ColumnName);
      }

      public static string GetCaption(string id)
      {
        return (DataSetProcessor._captions[(object) id] ?? (object) id).ToString();
      }

      /// <summary>Вычитание массивов Int64</summary>
      /// <param name="inArray"></param>
      /// <param name="filterArray"></param>
      /// <returns></returns>
      public static long[] DifferenceArray(long[] inArray, long[] filterArray)
      {
        ArrayList arrayList1 = new ArrayList();
        if (inArray != null && inArray.Length != 0)
        {
          if (filterArray != null && filterArray.Length != 0)
          {
            ArrayList arrayList2 = new ArrayList((ICollection) filterArray);
            if (arrayList2.Count > 1)
              arrayList2.Sort();
            foreach (long num in inArray)
            {
              if (arrayList2.BinarySearch((object) num) < 0)
                arrayList1.Add((object) num);
            }
          }
          else
            arrayList1.AddRange((ICollection) inArray);
        }
        return (long[]) arrayList1.ToArray(typeof (long));
      }

      public static string ConstructFilter(
        ConditionStructure[] conditions,
        IDBAttributeType[] attributeTypes)
      {
        return DataSetProcessor.ConstructFilter(conditions, attributeTypes, false);
      }

      /// <summary>
      /// Сформировать строку фильтрации по известным ConditionStructure и
      /// массиву IDBAttributeType для атрибутов, присутствующих в ConditionStructure.
      /// </summary>
      /// <param name="conditions">идентификация ConditionStructure.Attribute должна быть по int или guid</param>
      /// <param name="attributeTypes"></param>
      /// <param name="colGuids"></param>
      /// <returns></returns>
      public static string ConstructFilter(
        ConditionStructure[] conditions,
        IDBAttributeType[] attributeTypes,
        bool colGuids)
      {
        if (conditions == null)
          return string.Empty;
        string empty1 = string.Empty;
        for (int index1 = 0; index1 < conditions.Length; ++index1)
        {
          ConditionStructure condition = conditions[index1];
          IDBAttributeType attributeTypeById = DataSetProcessor.GetAttributeTypeByID(condition.Attribute, attributeTypes);
          if (attributeTypeById == null)
            throw new KernelExceptionID(231, condition.Attribute);
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          if (condition.GroupID > 0)
          {
            for (int index2 = 0; index2 < condition.GroupID; ++index2)
              empty2 += "(";
          }
          string str1 = empty2 + "(";
          string format = RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator);
          string str2;
          if (format == "")
          {
            str2 = str1 + "1=1";
          }
          else
          {
            FieldTypes fieldTypes = attributeTypeById.AttributeType;
            if (fieldTypes == FieldTypes.ftSystem)
              fieldTypes = ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeTypeById.AttributeID);
            string str3;
            AttributeTypeProperties propertiesStructure;
            if (!condition.CaseSensitive && (fieldTypes == FieldTypes.ftString || fieldTypes == FieldTypes.ftGuid || fieldTypes == FieldTypes.ftFile))
              str3 = $"{empty3}UPPER([{condition.Attribute.ToString()}])";
            else if (fieldTypes == FieldTypes.ftMeasured)
            {
              string str4 = empty3;
              string str5;
              if (!colGuids)
              {
                str5 = condition.Attribute.ToString();
              }
              else
              {
                propertiesStructure = attributeTypeById.PropertiesStructure;
                str5 = propertiesStructure.AttributeGuid.ToString();
              }
              str3 = $"{str4}[{str5}_BS]";
            }
            else
            {
              string str6 = empty3;
              string str7;
              if (!colGuids)
              {
                str7 = condition.Attribute.ToString();
              }
              else
              {
                propertiesStructure = attributeTypeById.PropertiesStructure;
                str7 = propertiesStructure.AttributeGuid.ToString();
              }
              str3 = $"{str6}[{str7}]";
            }
            object obj1 = (object) null;
            switch (fieldTypes)
            {
              case FieldTypes.ftString:
              case FieldTypes.ftDateTime:
              case FieldTypes.ftFile:
              case FieldTypes.ftGuid:
                if (condition.RelationalOperator == RelationalOperators.In && condition.Value is object[])
                {
                  object obj2 = (object) string.Empty;
                  object[] objArray = (object[]) condition.Value;
                  bool flag = false;
                  for (int index3 = 0; index3 < objArray.Length; ++index3)
                  {
                    object obj3 = objArray[index3] != null ? (object) $"'{Convert.ToString(objArray[index3])}'" : objArray[index3];
                    if (!condition.CaseSensitive && obj3 != null)
                      obj3 = (object) ((string) obj3).ToUpper();
                    if (obj3 != null)
                    {
                      if (flag)
                        obj2 = (object) (obj2.ToString() + ",");
                      obj2 = (object) (obj2.ToString() + (string) obj3);
                    }
                    flag = true;
                  }
                }
                object obj4;
                if (condition.RelationalOperator == RelationalOperators.LastNDays)
                {
                  obj4 = (object) (DateTime.Now - TimeSpan.FromDays((double) Convert.ToInt32(condition.Value))).ToString("dd.MM.yyyy 0:00:00");
                  format = " >= '{0}'";
                }
                else
                {
                  if (condition.Value == null)
                  {
                    obj4 = (object) null;
                    if (condition.RelationalOperator == RelationalOperators.NotEmpty)
                      format = $"{format} AND {str3}<>''";
                    else if (condition.RelationalOperator == RelationalOperators.Empty)
                      format = $"{format} OR {str3}=''";
                  }
                  else
                  {
                    string str8 = Convert.ToString(condition.Value);
                    switch (condition.RelationalOperator)
                    {
                      case RelationalOperators.Substring:
                      case RelationalOperators.NotSubstring:
                        str8 = str8 != string.Empty ? $"%{str8}%" : "%";
                        break;
                      case RelationalOperators.StartString:
                      case RelationalOperators.NotStartString:
                        str8 = str8 != string.Empty ? $"{str8}%" : "%";
                        break;
                      case RelationalOperators.EndString:
                      case RelationalOperators.NotEndString:
                        str8 = str8 != string.Empty ? $"%{str8}" : "%";
                        break;
                    }
                    obj4 = (object) $"'{str8}'";
                    if (!condition.CaseSensitive)
                      obj4 = (object) ((string) obj4).ToUpper();
                  }
                  obj1 = condition.Value2 != null ? (object) $"'{Convert.ToString(condition.Value2)}'" : condition.Value2;
                  if (!condition.CaseSensitive && obj1 != null)
                    obj1 = (object) ((string) obj1).ToUpper();
                }
                if (condition.RelationalOperator == RelationalOperators.Between)
                {
                  str2 = str1 + string.Format("{0} >= {1} AND {0} <= {2}", (object) str3, obj4, obj1);
                  break;
                }
                string str9 = string.Format(format, obj4, obj1);
                str2 = str1 + str3 + str9;
                break;
              case FieldTypes.ftBoolean:
                object obj5 = condition.Value == null || (!(condition.Value is bool) || !(bool) condition.Value) && (!(condition.Value is int) || (int) condition.Value != 1) && (!(condition.Value is string) || !((string) condition.Value).ToUpper().Equals("TRUE")) ? (object) 0 : (object) 1;
                object obj6 = condition.Value2 == null || (!(condition.Value2 is bool) || !(bool) condition.Value2) && (!(condition.Value2 is int) || (int) condition.Value2 != 1) && (!(condition.Value2 is string) || !((string) condition.Value2).ToUpper().Equals("TRUE")) ? (object) 0 : (object) 1;
                string str10 = string.Format(format, obj5, obj6);
                str2 = str1 + str3 + str10;
                break;
              case FieldTypes.ftMeasured:
                double num1;
                if (condition.RelationalOperator == RelationalOperators.In && condition.Value is object[])
                {
                  object obj7 = (object) string.Empty;
                  object[] objArray = (object[]) condition.Value;
                  bool flag = false;
                  long physicalQuantityID = -1;
                  long num2 = -1;
                  for (int index4 = 0; index4 < objArray.Length; ++index4)
                  {
                    object mValue = objArray[index4];
                    switch (mValue)
                    {
                      case null:
                        if (mValue != null)
                        {
                          if (flag)
                            obj7 = (object) (obj7.ToString() + ",");
                          object obj8 = obj7;
                          num1 = ((MeasuredValue) mValue).Value;
                          string str11 = num1.ToString();
                          obj7 = (object) (obj8.ToString() + str11);
                        }
                        flag = true;
                        continue;
                      case string _:
                      case MeasuredValue _:
                        if (mValue is string)
                          mValue = (object) MeasureHelper.ConvertToMeasuredValue((string) mValue);
                        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor((MeasuredValue) mValue);
                        if (physicalQuantityID == -1L)
                        {
                          physicalQuantityID = descriptor.PhysicalQuantityID;
                          num2 = MeasureHelper.GetBaseMeasureID(physicalQuantityID);
                        }
                        else if (physicalQuantityID != descriptor.PhysicalQuantityID)
                          throw new KernelExceptionID(232, (object) attributeTypeById.Name);
                        if (((MeasuredValue) mValue).MeasureID != num2)
                        {
                          mValue = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) mValue);
                          goto case null;
                        }
                        goto case null;
                      default:
                        throw new KernelExceptionID(233, (object) mValue.ToString());
                    }
                  }
                  string str12 = string.Format(format, obj7, obj1);
                  str2 = $"{str1}({str3}{str12})AND([{condition.Attribute.ToString()}_MU]={num2.ToString()})";
                  break;
                }
                MeasureDescriptor measureDescriptor1 = (MeasureDescriptor) null;
                MeasureDescriptor measureDescriptor2 = (MeasureDescriptor) null;
                object mValue1 = condition.Value;
                switch (mValue1)
                {
                  case null:
                    object mValue2 = condition.Value2;
                    switch (mValue2)
                    {
                      case null:
                        if (measureDescriptor1 != null && measureDescriptor2 != null && measureDescriptor1.PhysicalQuantityID != measureDescriptor2.PhysicalQuantityID)
                          throw new KernelExceptionID(232, (object) attributeTypeById.Name);
                        long baseMeasureId = measureDescriptor1 != null ? MeasureHelper.GetBaseMeasureID(measureDescriptor1.PhysicalQuantityID) : 0L;
                        if (mValue1 != null && ((MeasuredValue) mValue1).MeasureID != baseMeasureId)
                          mValue1 = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) mValue1);
                        object obj9;
                        if (mValue1 == null)
                        {
                          obj9 = mValue1;
                        }
                        else
                        {
                          num1 = ((MeasuredValue) mValue1).Value;
                          obj9 = (object) num1.ToString();
                        }
                        object obj10 = obj9;
                        if (mValue2 != null && ((MeasuredValue) mValue2).MeasureID != baseMeasureId)
                          mValue2 = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) mValue2);
                        object obj11;
                        if (mValue2 == null)
                        {
                          obj11 = mValue2;
                        }
                        else
                        {
                          num1 = ((MeasuredValue) mValue2).Value;
                          obj11 = (object) num1.ToString();
                        }
                        object obj12 = obj11;
                        string str13 = string.Format(format, obj10, obj12);
                        str2 = $"{str1}({str3}{str13})";
                        if (baseMeasureId != 0L)
                        {
                          str2 = $"{str2}AND([{condition.Attribute.ToString()}_MU]={baseMeasureId.ToString()})";
                          break;
                        }
                        break;
                      case string _:
                      case MeasuredValue _:
                        if (mValue2 is string)
                          mValue2 = (object) MeasureHelper.ConvertToMeasuredValue((string) mValue2);
                        measureDescriptor2 = MeasureHelper.FindDescriptor((MeasuredValue) mValue2);
                        goto case null;
                      default:
                        throw new KernelExceptionID(233, (object) mValue2.ToString());
                    }
                    break;
                  case string _:
                  case MeasuredValue _:
                    if (mValue1 is string)
                      mValue1 = (object) MeasureHelper.ConvertToMeasuredValue((string) mValue1);
                    measureDescriptor1 = MeasureHelper.FindDescriptor((MeasuredValue) mValue1);
                    goto case null;
                  default:
                    throw new KernelExceptionID(233, (object) mValue1.ToString());
                }
              default:
                object obj13;
                if (condition.RelationalOperator == RelationalOperators.In && condition.Value is object[])
                {
                  obj13 = (object) string.Empty;
                  object[] objArray = (object[]) condition.Value;
                  bool flag = false;
                  for (int index5 = 0; index5 < objArray.Length; ++index5)
                  {
                    object obj14 = objArray[index5] != null ? (object) Convert.ToString(objArray[index5]) : objArray[index5];
                    if (obj14 != null)
                    {
                      if (flag)
                        obj13 = (object) (obj13.ToString() + ",");
                      obj13 = (object) (obj13.ToString() + (string) obj14);
                    }
                    flag = true;
                  }
                }
                else
                {
                  obj13 = condition.Value != null ? (object) Convert.ToString(condition.Value) : condition.Value;
                  obj1 = condition.Value2 != null ? (object) Convert.ToString(condition.Value2) : condition.Value2;
                }
                string str14 = string.Format(format, obj13, obj1);
                str2 = str1 + str3 + str14;
                break;
            }
          }
          string str15 = str2 + ")";
          if (condition.GroupID < 0)
          {
            for (int index6 = 0; index6 > condition.GroupID; --index6)
              str15 += ")";
          }
          if (index1 < conditions.Length - 1)
          {
            if (condition.LogicalOperator != LogicalOperators.AND && condition.LogicalOperator != LogicalOperators.OR)
              throw new Exception($"Невозможно сформировать строку фильтрации. Отсутствует логический оператор у условия ({str15}).");
            str15 += condition.LogicalOperator.ToString();
          }
          empty1 += str15;
        }
        return empty1;
      }

      /// <summary>
      /// Вернуть DataTable из строк, выбранных из sourceDataTable в соответствии с фильтром filterString.
      /// Колонки соответствуют атрибутам.
      /// 
      /// Внимание: для каждой колонки с атрибутом типа единиц измерения должны быть добавлены два новых поля:
      /// 1. с именем (id атрибута типа единица измерения)+"_BS", в котором должны содержаться значения, выраженные в базовых единицах измерения
      /// 2. с именем (id атрибута типа единица измерения)+"_MU", в котором должен быть сокладирован идентификатор базовой единицы измерения.
      /// 
      /// наименования колонок должны быть образованы от идентификаторов атрибутов ( int или guid ).
      /// необходимо учитывать, что filterString оперирует в своих условиях именами колонок, образованных от int и/или guid соотв. атрибутов.
      /// при несоответствии наименований колонок тем, что были использованы в фильтре (при получении через ConstructFilter)
      /// будут проблемы.
      /// </summary>
      /// <param name="filterString"></param>
      /// <param name="sourceDataTable"></param>
      /// <returns></returns>
      public static DataTable GetRowsByFilter(string filterString, DataTable sourceDataTable)
      {
        bool caseSensitive = sourceDataTable.CaseSensitive;
        Dictionary<DataRow, Dictionary<string, string>> dictionary1 = (Dictionary<DataRow, Dictionary<string, string>>) null;
        DataTable toTable = new DataTable();
        try
        {
          if (!sourceDataTable.CaseSensitive)
            sourceDataTable.CaseSensitive = true;
          toTable.RemotingFormat = SerializationFormat.Binary;
          for (int index = 0; index < sourceDataTable.Columns.Count; ++index)
            toTable.Columns.Add(new DataColumn(sourceDataTable.Columns[index].ColumnName, sourceDataTable.Columns[index].DataType, sourceDataTable.Columns[index].Expression, sourceDataTable.Columns[index].ColumnMapping));
          string pattern = "UPPER";
          if (filterString.Contains(pattern))
          {
            int count1 = new Regex("\\'[^\\']+\\'").Matches(filterString).Count;
            int count2 = new Regex(pattern).Matches(filterString).Count;
            MatchCollection matchCollection = new Regex("UPPER\\(\\[(?<column>\\w+)\\]\\)").Matches(filterString);
            List<string> stringList = new List<string>(matchCollection.Count);
            foreach (Match match in matchCollection)
            {
              string str = match.Groups["column"].Value;
              if (!stringList.Contains(str))
                stringList.Add(str);
              filterString = filterString.Replace(match.Value, $"[{str}]");
            }
            if (count1 == count2)
            {
              sourceDataTable.CaseSensitive = false;
            }
            else
            {
              dictionary1 = new Dictionary<DataRow, Dictionary<string, string>>(sourceDataTable.Rows.Count);
              for (int index1 = 0; index1 < sourceDataTable.Rows.Count; ++index1)
              {
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>(stringList.Count);
                for (int index2 = 0; index2 < stringList.Count; ++index2)
                {
                  string str = Convert.ToString(sourceDataTable.Rows[index1][stringList[index2]]);
                  dictionary2.Add(stringList[index2], str);
                  sourceDataTable.Rows[index1][stringList[index2]] = (object) str.ToUpper();
                }
                dictionary1.Add(sourceDataTable.Rows[index1], dictionary2);
              }
              sourceDataTable.AcceptChanges();
            }
          }
          DataRow[] fromRows = sourceDataTable.Select(filterString);
          DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
        }
        finally
        {
          if (sourceDataTable.CaseSensitive != caseSensitive)
            sourceDataTable.CaseSensitive = caseSensitive;
          if (dictionary1 != null)
          {
            foreach (KeyValuePair<DataRow, Dictionary<string, string>> keyValuePair1 in dictionary1)
            {
              foreach (KeyValuePair<string, string> keyValuePair2 in keyValuePair1.Value)
                keyValuePair1.Key[keyValuePair2.Key] = (object) keyValuePair2.Value;
            }
            sourceDataTable.AcceptChanges();
            dictionary1.Clear();
          }
        }
        return toTable;
      }

      private static IDBAttributeType GetAttributeTypeByID(object id, IDBAttributeType[] attributeTypes)
      {
        if (id == null || attributeTypes == null)
          return (IDBAttributeType) null;
        switch (id)
        {
          case int _:
          case Guid _:
            IDBAttributeType attributeTypeById = (IDBAttributeType) null;
            for (int index = 0; index < attributeTypes.Length; ++index)
            {
              switch (id)
              {
                case int num when attributeTypes[index].AttributeID == num:
    label_7:
                  attributeTypeById = attributeTypes[index];
                  goto label_10;
                case Guid g:
                  if (!(attributeTypes[index] as IDBGuid).GUID.Equals(g))
                    break;
                  goto label_7;
              }
            }
    label_10:
            return attributeTypeById;
          default:
            throw new Exception(LocalizationHolder.rm.GetString("Interfaces_142"));
        }
      }

      public static Hashtable ColumnCaptions
      {
        get
        {
          if (DataSetProcessor._captions == null)
          {
            DataSetProcessor._captions = new Hashtable();
            DataSetProcessor._captions[(object) "F_OBJECT_TYPE"] = (object) LocalizationHolder.rm.GetString("Interfaces_143");
            DataSetProcessor._captions[(object) "F_OBJ_TYPE_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_144");
            DataSetProcessor._captions[(object) "F_OBJ_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_145");
            DataSetProcessor._captions[(object) "F_ICON"] = (object) LocalizationHolder.rm.GetString("Interfaces_146");
            DataSetProcessor._captions[(object) "F_VERSIONABLE"] = (object) LocalizationHolder.rm.GetString("Interfaces_147");
            DataSetProcessor._captions[(object) "F_NOTE"] = (object) LocalizationHolder.rm.GetString("Interfaces_148");
            DataSetProcessor._captions[(object) "F_DEFAULT_RELATION"] = (object) LocalizationHolder.rm.GetString("Interfaces_149");
            DataSetProcessor._captions[(object) "F_GUID"] = (object) LocalizationHolder.rm.GetString("Interfaces_150");
            DataSetProcessor._captions[(object) "F_AREA_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_151");
            DataSetProcessor._captions[(object) "F_CAPTION_ATTRIBUTE"] = (object) LocalizationHolder.rm.GetString("Interfaces_152");
            DataSetProcessor._captions[(object) "F_ANY_ATTRIBUTES"] = (object) LocalizationHolder.rm.GetString("Interfaces_153");
            DataSetProcessor._captions[(object) "F_PUBLIC_LC"] = (object) LocalizationHolder.rm.GetString("Interfaces_154");
            DataSetProcessor._captions[(object) "F_SHORT_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_155");
            DataSetProcessor._captions[(object) "F_AREA_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_156");
            DataSetProcessor._captions[(object) "F_AREA_NOTE"] = (object) LocalizationHolder.rm.GetString("Interfaces_157");
            DataSetProcessor._captions[(object) "F_LEVEL_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_158");
            DataSetProcessor._captions[(object) "F_LEVEL_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_159");
            DataSetProcessor._captions[(object) "F_LITERA"] = (object) LocalizationHolder.rm.GetString("Interfaces_160");
            DataSetProcessor._captions[(object) "F_DEFAULT"] = (object) LocalizationHolder.rm.GetString("Interfaces_161");
            DataSetProcessor._captions[(object) "F_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_162");
            DataSetProcessor._captions[(object) "F_ALIAS"] = (object) LocalizationHolder.rm.GetString("Interfaces_163");
            DataSetProcessor._captions[(object) "F_ATTRIBUTE_TYPE"] = (object) LocalizationHolder.rm.GetString("Interfaces_164");
            DataSetProcessor._captions[(object) "F_DEFAULT_VALUE"] = (object) LocalizationHolder.rm.GetString("Interfaces_165");
            DataSetProcessor._captions[(object) "F_DEFAULT_DESCRIPT"] = DataSetProcessor._captions[(object) "F_DEFAULT_VALUE"];
            DataSetProcessor._captions[(object) "F_MULTIPLE_VALUED"] = (object) LocalizationHolder.rm.GetString("Interfaces_166");
            DataSetProcessor._captions[(object) "F_COMPUTED"] = (object) LocalizationHolder.rm.GetString("Interfaces_167");
            DataSetProcessor._captions[(object) "F_SIZE_TYPE"] = (object) LocalizationHolder.rm.GetString("Interfaces_168");
            DataSetProcessor._captions[(object) "F_TYPE_DESCRIPTION"] = DataSetProcessor._captions[(object) "F_SIZE_TYPE"];
            DataSetProcessor._captions[(object) "F_FORMULA"] = (object) LocalizationHolder.rm.GetString("Interfaces_169");
            DataSetProcessor._captions[(object) "F_UNIQUE"] = (object) LocalizationHolder.rm.GetString("Interfaces_170");
            DataSetProcessor._captions[(object) "F_LANGUAGE_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_171");
            DataSetProcessor._captions[(object) "F_LANGUAGE_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_172");
            DataSetProcessor._captions[(object) "F_GROUP_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_173");
            DataSetProcessor._captions[(object) "F_GROUP_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_174");
            DataSetProcessor._captions[(object) "F_RELATION_TYPE"] = (object) LocalizationHolder.rm.GetString("Interfaces_175");
            DataSetProcessor._captions[(object) "F_DESCRIPTION"] = (object) LocalizationHolder.rm.GetString("Interfaces_176");
            DataSetProcessor._captions[(object) "F_TYPE_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_177");
            DataSetProcessor._captions[(object) "F_REVERSE_NAME"] = (object) LocalizationHolder.rm.GetString("Interfaces_178");
            DataSetProcessor._captions[(object) "F_CHKOUTFILE"] = (object) LocalizationHolder.rm.GetString("Interfaces_179");
            DataSetProcessor._captions[(object) "F_RELATION_KIND"] = (object) LocalizationHolder.rm.GetString("Interfaces_180");
            DataSetProcessor._captions[(object) "F_SAVE_HISTORY"] = (object) LocalizationHolder.rm.GetString("Interfaces_181");
            DataSetProcessor._captions[(object) "F_ATTRIBUTE_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_182");
            DataSetProcessor._captions[(object) "F_INVIEW"] = (object) LocalizationHolder.rm.GetString("Interfaces_183");
            DataSetProcessor._captions[(object) "F_OBJ_CREATE"] = (object) LocalizationHolder.rm.GetString("Interfaces_184");
            DataSetProcessor._captions[(object) "F_PUBLIC"] = (object) LocalizationHolder.rm.GetString("Interfaces_185");
            DataSetProcessor._captions[(object) "F_OPTIONS"] = (object) LocalizationHolder.rm.GetString("Interfaces_186");
            DataSetProcessor._captions[(object) "F_MASK"] = (object) LocalizationHolder.rm.GetString("Interfaces_187");
            DataSetProcessor._captions[(object) "F_CONTENT"] = (object) LocalizationHolder.rm.GetString("Interfaces_188");
            DataSetProcessor._captions[(object) "F_DEL_TIME"] = (object) LocalizationHolder.rm.GetString("Interfaces_189");
            DataSetProcessor._captions[(object) "F_SOURCE_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_190");
            DataSetProcessor._captions[(object) "F_MASTER_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_191");
            DataSetProcessor._captions[(object) "F_READ"] = (object) LocalizationHolder.rm.GetString("Interfaces_192");
            DataSetProcessor._captions[(object) "F_WRITE"] = (object) LocalizationHolder.rm.GetString("Interfaces_193");
            DataSetProcessor._captions[(object) "F_SEEK"] = (object) LocalizationHolder.rm.GetString("Interfaces_194");
            DataSetProcessor._captions[(object) "F_OPTIMIZED"] = (object) LocalizationHolder.rm.GetString("Interfaces_195");
            DataSetProcessor._captions[(object) "F_READ_DURATION"] = (object) LocalizationHolder.rm.GetString("Interfaces_196");
            DataSetProcessor._captions[(object) "F_SEEK_DURATION"] = (object) LocalizationHolder.rm.GetString("Interfaces_197");
            DataSetProcessor._captions[(object) "F_WRITE_DURATION"] = (object) LocalizationHolder.rm.GetString("Interfaces_198");
            DataSetProcessor._captions[(object) "F_MODIFY_DATE"] = (object) LocalizationHolder.rm.GetString("Interfaces_199");
            DataSetProcessor._captions[(object) "F_SCHEMA_ID"] = (object) LocalizationHolder.rm.GetString("Interfaces_200");
            DataSetProcessor._captions[(object) "F_CULTURE_ID"] = (object) LocalizationHolder.rm.GetString("F_CULTURE_ID");
            DataSetProcessor._captions[(object) "F_REQUIRED"] = (object) LocalizationHolder.rm.GetString("F_REQUIRED");
            DataSetProcessor._captions[(object) "F_VALIDATION_RULE"] = (object) LocalizationHolder.rm.GetString("F_VALIDATION_RULE");
            DataSetProcessor._captions[(object) "F_USER_ID"] = (object) LocalizationHolder.rm.GetString("F_USER_ID");
            DataSetProcessor._captions[(object) "F_SNAPSHOT_DATE"] = (object) LocalizationHolder.rm.GetString("F_SNAPSHOT_DATE");
            DataSetProcessor._captions[(object) "F_SNAPSHOT_ID"] = (object) LocalizationHolder.rm.GetString("F_SNAPSHOT_ID");
            DataSetProcessor._captions[(object) "F_TYPE_DESCRIPTION"] = (object) LocalizationHolder.rm.GetString("F_TYPE_DESCRIPTION");
            DataSetProcessor._captions[(object) "F_DEFAULT_DESCRIPT"] = (object) LocalizationHolder.rm.GetString("F_DEFAULT_DESCRIPT");
            DataSetProcessor._captions[(object) "F_DRAW_DATA"] = (object) LocalizationHolder.rm.GetString("F_DRAW_DATA");
            DataSetProcessor._captions[(object) "F_STORAGE_ID"] = (object) LocalizationHolder.rm.GetString("F_STORAGE_ID");
          }
          return DataSetProcessor._captions;
        }
      }

      /// <summary>
      /// Преобразовать указанное значение в Int32-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде Int32, либо значение по умолчанию</returns>
      public static int GetInt32Value(object value, int defValue)
      {
        int result = defValue;
        switch (value)
        {
          case int int32Value:
            return int32Value;
          case long _:
          case Decimal _:
            return Convert.ToInt32(value);
          case null:
            result = defValue;
            break;
          default:
            if (value == DBNull.Value || !int.TryParse(value.ToString(), out result))
              goto case null;
            break;
        }
        return result;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Int32.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static int GetInt32Value(DataRow row, string columnName, int defValue)
      {
        int int32Value = defValue;
        if (row != null && row.Table.Columns.Contains(columnName))
        {
          object obj = row[columnName];
          if (obj == null || obj == DBNull.Value)
            return defValue;
          int32Value = DataSetProcessor.GetInt32Value(obj, defValue);
        }
        return int32Value;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Int32.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static int GetInt32Value(DataRow row, int columnIndex, int defValue)
      {
        int int32Value = defValue;
        if (row != null && row.Table.Columns.Count > columnIndex)
        {
          object obj = row[columnIndex];
          if (obj == null || obj == DBNull.Value)
            return defValue;
          int32Value = DataSetProcessor.GetInt32Value(obj, defValue);
        }
        return int32Value;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Int64.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static long GetInt64Value(DataRow row, string columnName, long defValue)
      {
        long int64Value = defValue;
        if (row != null && row.Table.Columns.Contains(columnName))
        {
          object obj = row[columnName];
          if (obj == null || obj == DBNull.Value)
            return defValue;
          int64Value = DataSetProcessor.GetInt64Value(obj, defValue);
        }
        return int64Value;
      }

      /// <summary>
      /// Преобразовать указанное значение в Int64-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде Int64, либо значение по умолчанию</returns>
      public static long GetInt64Value(object value, long defValue)
      {
        long result = defValue;
        switch (value)
        {
          case long int64Value:
            return int64Value;
          case int _:
          case Decimal _:
            return Convert.ToInt64(value);
          case null:
            result = defValue;
            break;
          default:
            if (value == DBNull.Value || !long.TryParse(value.ToString(), out result))
              goto case null;
            break;
        }
        return result;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Int64.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static long GetInt64Value(DataRow row, int columnIndex, long defValue)
      {
        long int64Value = defValue;
        if (row != null && row.Table.Columns.Count > columnIndex)
        {
          object obj = row[columnIndex];
          if (obj == null || obj == DBNull.Value)
            return defValue;
          int64Value = DataSetProcessor.GetInt64Value(obj, defValue);
        }
        return int64Value;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Double.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static double GetDoubleValue(DataRow row, string columnName, double defValue)
      {
        double result = defValue;
        if (row != null && row.Table.Columns.Contains(columnName))
        {
          object obj = row[columnName];
          if (obj == null || obj == DBNull.Value || !double.TryParse(obj.ToString(), out result))
            result = defValue;
        }
        return result;
      }

      /// <summary>
      /// Преобразовать указанное значение в Double-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде Double, либо значение по умолчанию</returns>
      public static double GetDoubleValue(object value, double defValue)
      {
        double result = defValue;
        if (value == null || value == DBNull.Value || !double.TryParse(value.ToString(), out result))
          result = defValue;
        return result;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Int64.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static double GetDoubleValue(DataRow row, int columnIndex, double defValue)
      {
        double result = defValue;
        if (row != null && row.Table.Columns.Count > columnIndex)
        {
          object obj = row[columnIndex];
          if (obj == null || obj == DBNull.Value || !double.TryParse(obj.ToString(), out result))
            result = defValue;
        }
        return result;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде DateTime.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static DateTime GetDateTimeValue(DataRow row, string columnName, DateTime defValue)
      {
        DateTime result = defValue;
        if (row != null && row.Table.Columns.Contains(columnName))
        {
          object obj = row[columnName];
          if (obj == null || obj == DBNull.Value || !DateTime.TryParse(obj.ToString(), out result))
            result = defValue;
        }
        return result;
      }

      /// <summary>
      /// Преобразовать указанное значение в DateTime-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде DateTime, либо значение по умолчанию</returns>
      public static DateTime GetDateTimeValue(object value, DateTime defValue)
      {
        DateTime result = defValue;
        if (value == null || value == DBNull.Value || !DateTime.TryParse(value.ToString(), out result))
          result = defValue;
        return result;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Int64.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static DateTime GetDateTimeValue(DataRow row, int columnIndex, DateTime defValue)
      {
        DateTime result = defValue;
        if (row != null && row.Table.Columns.Count > columnIndex)
        {
          object obj = row[columnIndex];
          if (obj == null || obj == DBNull.Value || !DateTime.TryParse(obj.ToString(), out result))
            result = defValue;
        }
        return result;
      }

      /// <summary>
      /// Преобразовать указанное значение в string-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде string, либо значение по умолчанию</returns>
      public static string GetStringValue(object value, string defValue)
      {
        string stringValue = defValue;
        if (value != null && value != DBNull.Value)
          stringValue = value.ToString();
        return stringValue;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде string.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static string GetStringValue(DataRow row, string columnName, string defValue)
      {
        string stringValue = defValue;
        if (row != null && row.Table.Columns.Contains(columnName))
        {
          object obj = row[columnName];
          if (obj != null && obj != DBNull.Value)
            stringValue = Convert.ToString(obj);
        }
        return stringValue;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде string.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static string GetStringValue(DataRow row, int columnIndex, string defValue)
      {
        string stringValue = defValue;
        if (row != null && row.Table.Columns.Count > columnIndex)
        {
          object obj = row[columnIndex];
          if (obj != null && obj != DBNull.Value)
            stringValue = Convert.ToString(obj);
        }
        return stringValue;
      }

      /// <summary>
      /// Преобразовать указанное значение в Guid-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде Guid, либо значение по умолчанию</returns>
      public static Guid GetGuidValue(object value, Guid defValue)
      {
        string str = defValue.ToString();
        if (value != null && value != DBNull.Value)
          str = value.ToString();
        return GuidHelper.IsGuid(str) ? new Guid(str) : defValue;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Guid.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static Guid GetGuidValue(DataRow row, string columnName, Guid defValue)
      {
        string str = defValue.ToString();
        if (row != null && row.Table.Columns.Contains(columnName))
        {
          object obj = row[columnName];
          if (obj != null && obj != DBNull.Value)
            str = Convert.ToString(obj);
          if (GuidHelper.IsGuid(str))
            return new Guid(str);
        }
        return defValue;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Guid.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static Guid GetGuidValue(DataRow row, int columnIndex, Guid defValue)
      {
        string str = defValue.ToString();
        if (row != null && row.Table.Columns.Count > columnIndex)
        {
          object obj = row[columnIndex];
          if (obj != null && obj != DBNull.Value)
            str = Convert.ToString(obj);
          if (GuidHelper.IsGuid(str))
            return new Guid(str);
        }
        return defValue;
      }

      /// <summary>
      /// Преобразовать указанное значение в MeasuredValue-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// Внимание! Пользоваться данным методом можно только после инициализации MeasureHelper!
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде MeasuredValue, либо значение по умолчанию</returns>
      public static MeasuredValue GetMeasuredValue(object value, MeasuredValue defValue)
      {
        MeasuredValue measuredValue = defValue;
        if (value == null || value == DBNull.Value)
          return measuredValue;
        if (value is MeasuredValue)
          return (MeasuredValue) value;
        try
        {
          return MeasureHelper.ConvertToMeasuredValue(value.ToString());
        }
        catch
        {
          return defValue;
        }
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде MeasuredValue.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static MeasuredValue GetMeasuredValue(
        DataRow row,
        string columnName,
        MeasuredValue defValue)
      {
        return row != null && row.Table.Columns.Contains(columnName) ? DataSetProcessor.GetMeasuredValue(row[columnName], defValue) : defValue;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде MeasuredValue.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static MeasuredValue GetMeasuredValue(
        DataRow row,
        int columnIndex,
        MeasuredValue defValue)
      {
        return row != null && row.Table.Columns.Count > columnIndex ? DataSetProcessor.GetMeasuredValue(row[columnIndex], defValue) : defValue;
      }

      /// <summary>
      /// Преобразовать указанное значение в Boolean-результат
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="value">Значение</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>Значение в виде Boolean, либо значение по умолчанию</returns>
      public static bool GetBooleanValue(object value, bool defValue)
      {
        if (value == null || value == DBNull.Value)
          return defValue;
        string s = value.ToString();
        bool result1;
        if (bool.TryParse(s, out result1))
          return result1;
        int result2;
        return !int.TryParse(s, out result2) ? defValue : result2 != 0;
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Boolean.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnName">Имя столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение Boolean умолчанию</returns>
      public static bool GetBooleanValue(DataRow row, string columnName, bool defValue)
      {
        return row != null && row.Table.Columns.Contains(columnName) ? DataSetProcessor.GetBooleanValue(row[columnName], defValue) : defValue;
      }

      public static bool GetBooleanValue(DataRow dataRow, int columnIndex, bool defaultValue)
      {
        if (dataRow == null)
          throw new ArgumentNullException(nameof (dataRow));
        if (columnIndex < 0 || columnIndex > dataRow.Table.Columns.Count - 1)
          throw new ArgumentException();
        return DataSetProcessor.GetBooleanValue(dataRow[columnIndex], defaultValue);
      }

      /// <summary>
      /// Получить значение из указанного столбца в строке в виде Boolean.
      /// При ошибке будет возвращено значение по умолчанию.
      /// </summary>
      /// <param name="row">Строка</param>
      /// <param name="columnIndex">Индекс столбца</param>
      /// <param name="defValue">Значение по умолчанию</param>
      /// <returns>значение из указанного столбца в строке, либо значение по умолчанию</returns>
      public static bool GetInt32Value(DataRow row, int columnIndex, bool defValue)
      {
        bool result = defValue;
        if (row != null && row.Table.Columns.Count > columnIndex)
        {
          object obj = row[columnIndex];
          if (obj == null || obj == DBNull.Value || !bool.TryParse(obj.ToString(), out result))
            result = defValue;
        }
        return result;
      }

      /// <summary>
      /// Получить список Int32-идентификаторов атрибутов, по которым идёт сортировка.
      /// ВНИМАНИЕ! Метод работает при корректно загруженном кэше MetaDataHelper!
      /// </summary>
      /// <param name="selectParams">Параметры запроса, по которым была получена таблица</param>
      /// <param name="sortedAttrs">В данный словарик будут размещены все атрибуты (Int32), и способ их сортировки</param>
      public static void AttributeFindSortOrders(
        DBRecordSetParams selectParams,
        ref Dictionary<int, SortOrders> sortedAttrs)
      {
        if (sortedAttrs == null)
          sortedAttrs = new Dictionary<int, SortOrders>();
        sortedAttrs.Clear();
        if (selectParams.Columns == null || selectParams.ColumnNames == null || selectParams.SortColumns == null)
          return;
        List<SortOrders> sortOrdersList = new List<SortOrders>(selectParams.SortColumns.Length);
        if (selectParams.Orders == null)
        {
          for (int index = 0; index < selectParams.SortColumns.Length; ++index)
            sortOrdersList.Add(SortOrders.ASC);
        }
        else
        {
          for (int index = 0; index < selectParams.Orders.Length; ++index)
            sortOrdersList.Add(selectParams.Orders[index]);
        }
        if (selectParams.SortColumns.Length != sortOrdersList.Count)
          return;
        for (int index = 0; index < selectParams.SortColumns.Length; ++index)
        {
          int attributeId = MetaDataHelper.GetAttributeID(selectParams.SortColumns[index]);
          if (!sortedAttrs.ContainsKey(attributeId))
            sortedAttrs.Add(attributeId, sortOrdersList[index]);
        }
      }

      /// <summary>
      /// Метод позволяет отыскать все столбцы в таблице, в которых располагаются значения
      /// указанного атрибута (способ именования столбцов роли не играет).
      /// ВНИМАНИЕ! Метод работает при корректно загруженном кэше MetaDataHelper!
      /// </summary>
      /// <param name="selectParams">Параметры запроса, по которым была получена таблица</param>
      /// <param name="table">Таблица, колонки которой изучаются</param>
      /// <param name="attrID">Идентификатор атрибута (Int32, Guid, string)</param>
      /// <param name="AttributeSource">Источник атрибута (объект, связь, событие)</param>
      /// <param name="columnsAttrs">В данный словарик будет размещён кэш атрибутов и их столбцов в таблице (ускоряет обработку)</param>
      public static void AttributeFindColumns(
        DBRecordSetParams selectParams,
        DataTable table,
        object attrID,
        AttributeSourceTypes AttributeSource,
        ref Dictionary<object, List<int>> columnsAttrs)
      {
        if (columnsAttrs == null)
          columnsAttrs = new Dictionary<object, List<int>>();
        if (selectParams.Columns == null || selectParams.ColumnNames == null || columnsAttrs.ContainsKey(attrID))
          return;
        List<int> intList = new List<int>();
        columnsAttrs.Add(attrID, intList);
        int attributeId = MetaDataHelper.GetAttributeID(attrID);
        if (attributeId == -10000)
          return;
        for (int index = 0; index < selectParams.Columns.Length; ++index)
        {
          if (MetaDataHelper.GetAttributeID(selectParams.Columns[index]) == attributeId)
            intList.Add(index);
        }
      }

      /// <summary>
      /// Выполнить замену значения указанного атрибута в строке таблицы на новое значение.
      /// ВНИМАНИЕ! Метод работает при корректно загруженном кэше MetaDataHelper!
      /// </summary>
      /// <param name="selectParams">Параметры запроса, по которым была получена таблица</param>
      /// <param name="attrID">Идентификатор атрибута (Int32, Guid, string)</param>
      /// <param name="attributeSource">Источник атрибута (объект, связь, событие)</param>
      /// <param name="row">Строка, в которой могут быть значения указанного атрибута</param>
      /// <param name="newValue">Новое значение</param>
      /// <param name="columnsAttrs">В данный словарик будет размещён кэш атрибутов и их столбцов в таблице (ускоряет обработку)</param>
      /// <returns>Количество выполненных замен</returns>
      public virtual int AttributeReplaceValue(
        DBRecordSetParams selectParams,
        object attrID,
        AttributeSourceTypes attributeSource,
        DataRow row,
        object newValue,
        ref Dictionary<object, List<int>> columnsAttrs)
      {
        if (attrID == null || row == null)
          return 0;
        DataTable table = row.Table;
        if (table == null || table.Columns.Count == 0 || table.Rows.Count == 0)
          return 0;
        DataSetProcessor.AttributeFindColumns(selectParams, table, attrID, attributeSource, ref columnsAttrs);
        if (!columnsAttrs.ContainsKey(attrID))
          return 0;
        List<int> intList = columnsAttrs[attrID];
        for (int index = 0; index < intList.Count; ++index)
          row[intList[index]] = newValue;
        return intList.Count;
      }

      /// <summary>
      /// Получить индекс столбца в результирующей таблице по параметрам запроса.
      /// ВНИМАНИЕ! Метод работает при корректно загруженном кэше MetaDataHelper!
      /// </summary>
      /// <param name="pars">Анализируемые параметры запроса</param>
      /// <param name="AttributeID">Идентификатор атрибута (Int32, Guid, string)</param>
      /// <param name="AttributeSource">Источник атрибута (объект, связь, событие)</param>
      /// <returns>-1, если указанный атрибут не был запрошен</returns>
      public static int AttributeColumnIndex(
        DBRecordSetParams pars,
        object AttributeID,
        AttributeSourceTypes AttributeSource)
      {
        return DataSetProcessor.AttributeColumnIndex(pars, AttributeID, AttributeSource, (DataTable) null);
      }

      /// <summary>
      /// Получить индекс столбца в результирующей таблице по параметрам запроса.
      /// ВНИМАНИЕ! Метод работает при корректно загруженном кэше MetaDataHelper!
      /// </summary>
      /// <param name="pars">Анализируемые параметры запроса</param>
      /// <param name="AttributeID">Идентификатор атрибута (Int32, Guid, string)</param>
      /// <param name="AttributeSource">Источник атрибута (объект, связь, событие)</param>
      /// <param name="table">Таблица, в которой выполняется поиск (позволяет скорректировать индекс)</param>
      /// <returns>-1, если указанный атрибут не был запрошен</returns>
      public static int AttributeColumnIndex(
        DBRecordSetParams pars,
        object AttributeID,
        AttributeSourceTypes AttributeSource,
        DataTable table)
      {
        if (pars.Columns == null || pars.Columns.Length == 0 || AttributeID == null)
          return -1;
        int attributeId1 = MetaDataHelper.GetAttributeID(AttributeID);
        if (attributeId1 == -10000)
          return -1;
        int num1 = table != null ? Math.Min(table.Columns.Count - pars.Columns.Length, 0) : 0;
        int num2 = table != null ? Math.Min(table.Columns.Count, pars.Columns.Length) : pars.Columns.Length;
        for (int index = 0; index < pars.Columns.Length; ++index)
        {
          int attributeId2 = MetaDataHelper.GetAttributeID(pars.Columns[index]);
          if (attributeId2 != -10000 && attributeId2 == attributeId1)
          {
            AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Auto;
            if (pars.ColumnsInfo != null && pars.ColumnsInfo.Length > index)
              attributeSourceTypes = pars.ColumnsInfo[index].AttributeSource;
            if (attributeSourceTypes == AttributeSource)
            {
              int num3 = index;
              if (num3 >= num2)
                num3 += num1;
              return num3;
            }
          }
        }
        return -1;
      }

      /// <summary>
      /// Проверить, запрошен ли указанный атрибут у указанного источника.
      /// ВНИМАНИЕ! Метод работает при корректно загруженном кэше MetaDataHelper!
      /// </summary>
      /// <param name="pars">Анализируемые параметры запроса</param>
      /// <param name="AttributeID">Идентификатор атрибута (Int32, Guid, string)</param>
      /// <param name="AttributeSource">Источник атрибута (объект, связь, событие)</param>
      /// <returns>true, если столбец был найден в параметрах запроса</returns>
      public static bool AttributeColumnExists(
        DBRecordSetParams pars,
        object AttributeID,
        AttributeSourceTypes AttributeSource)
      {
        if (pars.Columns == null || pars.Columns.Length == 0 || AttributeID == null)
          return false;
        int attributeId1 = MetaDataHelper.GetAttributeID(AttributeID);
        if (attributeId1 == -1)
          return false;
        for (int index = 0; index < pars.Columns.Length; ++index)
        {
          int attributeId2 = MetaDataHelper.GetAttributeID(pars.Columns[index]);
          if (attributeId2 != -10000 && attributeId2 == attributeId1)
          {
            AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Auto;
            if (pars.ColumnsInfo != null && pars.ColumnsInfo.Length > index)
              attributeSourceTypes = pars.ColumnsInfo[index].AttributeSource;
            if (attributeSourceTypes == AttributeSource)
              return true;
          }
        }
        return false;
      }

      /// <summary>
      /// Найти в указанной таблице столбец с атрибутом по имени атрибута, его идентификатору или его Guid
      /// </summary>
      /// <param name="source">Таблица, в колонках которой разыскивается атрибут</param>
      /// <param name="attribute">int, string, guid - идентификатор, название или guid разыскиваемого атрибута</param>
      /// <returns>-1, если атрибут не найден, иначе порядковый номер колонки атрибута в таблице</returns>
      public static int AttributeColumnID(DataTable source, object attribute)
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
      /// Выполнить фильтрацию версий объектов, входящих в контексты редактирования.
      /// В таблице останутся версии объектов, не входящие в контексты редактирования,
      /// версии, принадлежащие текущему контексту редактирования, а также базовые
      /// версии объектов независимо от их принадлежности контексту редактирования.
      /// Если текущий контекст является упрощённым, то все контекстные версии,
      /// принадлежащие текущему контексту редактирования, будут оставлены в таблице
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="table">Фильтруемая таблица</param>
      /// <param name="function">Идентификатор функции, которая вызвала метод Select</param>
      /// <param name="context">Текущий контекст редактирования (или null, если он не является упрощённым, либо отключён)</param>
      /// <returns>Отфильтрованные версии объектов</returns>
      public static DataTable FiltrateContextVersions(
        IUserSession session,
        DataTable table,
        SelectFunction function,
        EditingContextsObjectContainer context)
      {
        if (table == null || table.Rows.Count == 0 || table.Columns.Count < 3 || session == null)
          return table;
        long num = Math.Abs(session.EditingContextModificationID);
        int columnIndex1 = table.Columns.IndexOf("cad00029-306c-11d8-b4e9-00304f19f545");
        int columnIndex2 = table.Columns.IndexOf("cad014d3-306c-11d8-b4e9-00304f19f545");
        int columnIndex3 = table.Columns.IndexOf("cad014d2-306c-11d8-b4e9-00304f19f545");
        if (columnIndex1 < 0 || columnIndex2 < 0 || columnIndex3 < 0)
          return table;
        long extendedProperty = table.ExtendedProperties.ContainsKey((object) "Part_Object_ID") ? (long) table.ExtendedProperties[(object) "Part_Object_ID"] : 0L;
        int columnIndex4 = table.Columns.IndexOf("cad001c2-306c-11d8-b4e9-00304f19f545");
        bool flag = columnIndex4 >= 0;
        DataTable dataTable = table.Clone();
        int count = table.Columns.Count;
        object[] buffer = new object[count];
        dataTable.RemotingFormat = SerializationFormat.Binary;
        for (int index = 0; index < table.Rows.Count; ++index)
        {
          DataRow row = table.Rows[index];
          long versionID = DataSetProcessor.GetInt64Value(row, columnIndex1, 0L);
          if ((DataSetProcessor.GetInt64Value(row, columnIndex2, 0L) & 1L) == 1L)
          {
            DataSetProcessor.CopyDataToBuffer(row, buffer, count);
            dataTable.Rows.Add(buffer);
          }
          else
          {
            if (flag)
            {
              long int64Value = DataSetProcessor.GetInt64Value(row, columnIndex4, 0L);
              if (function == SelectFunction.EntersInVersion)
                versionID = extendedProperty;
              if (Math.Abs(int64Value) == Math.Abs(versionID))
              {
                DataSetProcessor.CopyDataToBuffer(row, buffer, count);
                dataTable.Rows.Add(buffer);
                continue;
              }
            }
            long int64Value1 = DataSetProcessor.GetInt64Value(row, columnIndex3, 0L);
            if (int64Value1 == 0L || Math.Abs(int64Value1) == num || context != null && !context.ExistsVersion(versionID))
            {
              DataSetProcessor.CopyDataToBuffer(row, buffer, count);
              dataTable.Rows.Add(buffer);
            }
          }
        }
        dataTable.AcceptChanges();
        return dataTable;
      }

      /// <summary>
      /// Подготовить класс настроек для выполнения фильтрации версий по сериям изделий и датам выпуска/действия
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="table">Таблица</param>
      /// <returns>Класс настроек или null, если фильтрация не разрешена или невозможна</returns>
      private static SeriesDateSettings GetSeriesDateSettings(IUserSession session, DataTable table)
      {
        SeriesDateSettings seriesDateSettings = (SeriesDateSettings) null;
        if (session == null || table == null)
          return seriesDateSettings;
        string columnName = "cadd940c-306c-11d8-b4e9-00304f19f545";
        if (table.Columns.IndexOf(columnName) < 0)
          return seriesDateSettings;
        DBRecordSetParams dbRecordSetParams = table.ExtendedProperties.ContainsKey((object) "DBRecordSetParams") ? (DBRecordSetParams) table.ExtendedProperties[(object) "DBRecordSetParams"] : new DBRecordSetParams();
        settings = (SeriesDateSettingsHolder) null;
        bool enabled = false;
        if (dbRecordSetParams.Tags != null && dbRecordSetParams.Tags.Contains((object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}"))
          enabled = dbRecordSetParams.Tags[(object) "{E2390B62-E0BA-4F7E-89CC-1E9E33F0BB5C}"] is SeriesDateSettingsHolder settings && !settings.IsEmpty;
        if (dbRecordSetParams.Tags != null && dbRecordSetParams.Tags.Contains((object) "{02C00D9C-738E-42AB-A905-454BBD0644AD}") && Convert.ToBoolean(dbRecordSetParams.Tags[(object) "{02C00D9C-738E-42AB-A905-454BBD0644AD}"]))
          enabled = false;
        return !enabled || !(session.GetCustomService(typeof (IVersionApplicabilitiesService)) is IVersionApplicabilitiesService customService) ? seriesDateSettings : new SeriesDateSettings(settings, customService, enabled);
      }

      /// <summary>
      /// Выполнить фильтрацию версий объектов по сериям/датам.
      /// В таблице останутся версии объектов, которые удовлетворяют заданным условиям фильтрации
      /// по сериям/датам. Если подбор выключен, таблица изменяться не будет
      /// </summary>
      /// <param name="session">Сессия</param>
      /// <param name="table">Фильтруемая таблица</param>
      /// <param name="paramSet">Параметры запроса, по которым была получена таблица</param>
      /// <param name="function">Идентификатор функции, которая вызвала метод Select</param>
      /// <param name="services">Контейнер сервисов</param>
      /// <returns>Отфильтрованные версии объектов</returns>
      public static DataTable FiltrateSeriesVersions(
        IUserSession session,
        DataTable table,
        DBRecordSetParams paramSet,
        SelectFunction function,
        IServiceProvider services)
      {
        if (table == null || table.Rows.Count == 0 || table.Columns.Count < 3 || session == null)
          return table;
        int columnIndex1 = DataSetProcessor.AttributeColumnIndex(paramSet, (object) -3, AttributeSourceTypes.Object, table);
        int columnIndex2 = DataSetProcessor.AttributeColumnIndex(paramSet, (object) -2, AttributeSourceTypes.Object, table);
        int columnIndex3 = DataSetProcessor.AttributeColumnIndex(paramSet, (object) MetaDataHelper.GetAttributeTypeID("cadd940c-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, table);
        if (columnIndex3 < 0 || columnIndex2 < 0 || columnIndex1 < 0)
          return table;
        SeriesDateSettings seriesDateSettings = DataSetProcessor.GetSeriesDateSettings(session, table);
        if (seriesDateSettings == null || !seriesDateSettings.Enabled || seriesDateSettings.Settings == null || seriesDateSettings.Settings.IsEmpty)
          return table;
        SortedDictionary<long, List<int>> sortedDictionary1 = new SortedDictionary<long, List<int>>();
        SortedDictionary<int, bool> parsedRows = new SortedDictionary<int, bool>();
        SortedDictionary<int, bool> sortedDictionary2 = new SortedDictionary<int, bool>();
        IElementStatusesService service = services != null ? services.GetService(typeof (IElementStatusesService)) as IElementStatusesService : (IElementStatusesService) null;
        int statusesColumnIndex = ElementStatusesPluginDescription.GetStatusesColumnIndex(ref table);
        bool flag = statusesColumnIndex >= 0 && service != null;
        for (int index = 0; index < table.Rows.Count; ++index)
        {
          DataRow row = table.Rows[index];
          long int64Value1 = DataSetProcessor.GetInt64Value(row[columnIndex1], 0L);
          long int64Value2 = DataSetProcessor.GetInt64Value(row[columnIndex2], 0L);
          if (int64Value1 == int64Value2 && int64Value1 == 0L)
          {
            sortedDictionary2[index] = true;
          }
          else
          {
            if (!sortedDictionary1.ContainsKey(int64Value1))
              sortedDictionary1[int64Value1] = new List<int>();
            sortedDictionary1[int64Value1].Add(index);
          }
        }
        DataTable dataTable = table.Clone();
        dataTable.RemotingFormat = SerializationFormat.Binary;
        foreach (KeyValuePair<long, List<int>> keyValuePair in sortedDictionary1)
        {
          List<DataSetProcessor.VersionsWeight> versionsWeightList = new List<DataSetProcessor.VersionsWeight>(keyValuePair.Value.Count);
          for (int index = 0; index < keyValuePair.Value.Count; ++index)
          {
            int num = keyValuePair.Value[index];
            DataRow row = table.Rows[num];
            long int64Value = DataSetProcessor.GetInt64Value(row, columnIndex2, 0L);
            ObjectFiltrationState state = seriesDateSettings.CheckApplicabilities(session, DataSetProcessor.GetStringValue(row, columnIndex3, string.Empty), int64Value);
            versionsWeightList.Add(new DataSetProcessor.VersionsWeight(int64Value, state, num));
          }
          versionsWeightList.Sort();
          DataSetProcessor.VersionsWeight versionsWeight = versionsWeightList.Find((Predicate<DataSetProcessor.VersionsWeight>) (item => item.Weight < 2));
          if (versionsWeight != null)
          {
            parsedRows[versionsWeight.RowIndex] = true;
            if (flag)
              service.SetElementStatuses16("{14BE37A7-84F7-44CB-97AA-15A713C703E0}", table.Rows[versionsWeight.RowIndex][statusesColumnIndex] as byte[], Convert.ToInt16((object) versionsWeight.State));
          }
          else
            versionsWeightList.ForEach((Action<DataSetProcessor.VersionsWeight>) (item => parsedRows[item.RowIndex] = false));
        }
        for (int index = 0; index < table.Rows.Count; ++index)
        {
          if (parsedRows.ContainsKey(index))
          {
            DataRow row = table.Rows[index];
            dataTable.Rows.Add(row.ItemArray);
          }
        }
        dataTable.AcceptChanges();
        return dataTable;
      }

      /// <summary>
      /// Выполнить сортировку таблицы по указанному целочисленному атрибуту
      /// </summary>
      /// <param name="table">Фильтруемая таблица</param>
      /// <param name="attrColumnIndex">Номер сортируемой колонки</param>
      /// <returns>Таблица с отсортированными полями</returns>
      public static void SortDataTableByIntegerAttribute(DataTable table, int attrColumnIndex)
      {
        if (table == null || table.Columns.Count == 0 || table.Rows.Count <= 1 || attrColumnIndex < 0 || attrColumnIndex >= table.Columns.Count)
          return;
        SortedDictionary<long, List<DataRow>> sortedDictionary = new SortedDictionary<long, List<DataRow>>();
        int count1 = table.Rows.Count;
        for (int index = 0; index < count1; ++index)
        {
          DataRow row = table.Rows[index];
          long int64Value = DataSetProcessor.GetInt64Value(row, attrColumnIndex, 0L);
          if (!sortedDictionary.ContainsKey(int64Value))
            sortedDictionary.Add(int64Value, new List<DataRow>());
          sortedDictionary[int64Value].Add(row);
        }
        List<DataRow> dataRowList = new List<DataRow>(count1);
        foreach (KeyValuePair<long, List<DataRow>> keyValuePair in sortedDictionary)
          dataRowList.AddRange((IEnumerable<DataRow>) keyValuePair.Value);
        int count2 = table.Columns.Count;
        object[] buffer = new object[count2];
        for (int index = 0; index < dataRowList.Count; ++index)
        {
          DataSetProcessor.CopyDataToBuffer(dataRowList[index], buffer, count2);
          table.Rows.Add(buffer);
          table.Rows.Remove(dataRowList[index]);
        }
      }

      /// <summary>
      /// Скопировать указанную строку с данными fromRow в указанную строку toRow
      /// </summary>
      /// <param name="fromRow">Исходная строка с данными</param>
      /// <param name="toRow">Строка-получатель</param>
      internal static void CopyRow(DataRow fromRow, DataRow toRow)
      {
        if (fromRow == null || toRow == null)
          return;
        int count = fromRow.Table.Columns.Count;
        for (int columnIndex = 0; columnIndex < count; ++columnIndex)
          toRow[columnIndex] = fromRow[columnIndex];
      }

      public static void DataTableToCSV(DataTable dtDataTable, string strFilePath)
      {
        StreamWriter streamWriter = new StreamWriter(strFilePath, false);
        for (int index = 0; index < dtDataTable.Columns.Count; ++index)
        {
          streamWriter.Write((object) dtDataTable.Columns[index]);
          if (index < dtDataTable.Columns.Count - 1)
            streamWriter.Write(",");
        }
        streamWriter.Write(streamWriter.NewLine);
        foreach (DataRow row in (InternalDataCollectionBase) dtDataTable.Rows)
        {
          for (int columnIndex = 0; columnIndex < dtDataTable.Columns.Count; ++columnIndex)
          {
            if (!Convert.IsDBNull(row[columnIndex]))
            {
              string source = row[columnIndex].ToString();
              if (source.Contains<char>(','))
              {
                string str = $"\"{source}\"";
                streamWriter.Write(str);
              }
              else
                streamWriter.Write(row[columnIndex].ToString());
            }
            if (columnIndex < dtDataTable.Columns.Count - 1)
              streamWriter.Write(",");
          }
          streamWriter.Write(streamWriter.NewLine);
        }
        streamWriter.Close();
      }

      /// <summary>
      /// Класс, в котором хранится идентификатор версии объекта, номер её строки в таблице и
      /// удельный "вес" после подбора по сериям/датам (чем "тяжелее" версия, тем менее удачной она
      /// является в плане подбора)
      /// </summary>
      private sealed class VersionsWeight : IComparable<DataSetProcessor.VersionsWeight>
      {
        /// <summary>Идентификатор версии объекта</summary>
        internal long ObjectID;
        /// <summary>Результат подбора версии по сериям/датам</summary>
        internal ObjectFiltrationState State;
        /// <summary>Индекс строки</summary>
        internal int RowIndex;

        /// <summary>Удельный "вес" версии после подбора</summary>
        internal int Weight => SeriesDatesHelper.GetWeight(this.State);

        /// <summary>Создать описание версии объекта</summary>
        /// <param name="objectID">Идентификатор версии объекта</param>
        /// <param name="state">Результат подбора версии по сериям/датам</param>
        /// <param name="rowIndex">Индекс строки</param>
        public VersionsWeight(long objectID, ObjectFiltrationState state, int rowIndex)
        {
          this.ObjectID = objectID;
          this.State = state;
          this.RowIndex = rowIndex;
        }

        /// <summary>Сравнить с указанным объектом</summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>true - объекты равны</returns>
        public override bool Equals(object obj)
        {
          return this.CompareTo(obj as DataSetProcessor.VersionsWeight) == 0;
        }

        /// <summary>Получить 32-битный хэш-код экземпляра класса</summary>
        /// <returns>32-битный хэш-код экземпляра класса</returns>
        public override int GetHashCode() => this.ObjectID.GetHashCode() << 2 | this.Weight;

        /// <summary>Сравнить с указанным объектом</summary>
        /// <param name="other">Другой объект для сравнения</param>
        /// <returns>-1, 0, 1</returns>
        public int CompareTo(DataSetProcessor.VersionsWeight other)
        {
          if (other == null)
            return -1;
          if (this == other)
            return 0;
          int num = this.Weight.CompareTo(other.Weight);
          return num != 0 ? num : this.RowIndex.CompareTo(other.RowIndex);
        }
      }
    }
}
