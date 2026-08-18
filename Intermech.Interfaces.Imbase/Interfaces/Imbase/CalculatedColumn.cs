// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.CalculatedColumn
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Expressions;
using Intermech.Imbase;
using Intermech.Interfaces.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Imbase;

public class CalculatedColumn
{
  public int _columnIndex;
  public string _columnName;
  public List<int> _dependColumnsIndex;
  private ExpressionTree _tree;

  public CalculatedColumn(ExpressionTree tree, string columnName, DataTable dataTable)
  {
    this._tree = tree;
    this._columnName = columnName;
    this._dependColumnsIndex = new List<int>(0);
    this.UpdateIndexes(dataTable);
  }

  public static CalculatedColumn[] Sort(CalculatedColumn[] list, ref int cycledColumnIndex)
  {
    cycledColumnIndex = CalculatedColumn.CheckCycling(list);
    if (cycledColumnIndex != -1)
      return list;
    int index1 = 0;
    int num1 = list.Length * 2;
    int num2 = 0;
    while (index1 < list.Length - 1)
    {
      List<int> dependColumnsIndex = list[index1]._dependColumnsIndex;
      bool flag = false;
      for (int index2 = index1 + 1; index2 < list.Length; ++index2)
      {
        for (int index3 = 0; index3 < dependColumnsIndex.Count; ++index3)
        {
          if (dependColumnsIndex[index3] == list[index2]._columnIndex)
          {
            CalculatedColumn calculatedColumn = list[index2];
            list[index2] = list[index1];
            list[index1] = calculatedColumn;
            flag = true;
            ++num2;
            break;
          }
        }
      }
      if (!flag)
        ++index1;
      if (num2 > num1)
        break;
    }
    return list;
  }

  private static int CheckCycling(CalculatedColumn[] list)
  {
    if (list == null || list.Length == 1)
      return -1;
    int length = list.Length;
    List<int> stack = new List<int>(32 /*0x20*/);
    Hashtable ht = new Hashtable(length);
    for (int index = 0; index < length; ++index)
      ht[(object) list[index]._columnIndex] = (object) list[index];
    for (int index = 0; index < length; ++index)
    {
      CalculatedColumn column = list[index];
      stack.Clear();
      if (CalculatedColumn.CheckColumnCycling(column, stack, ht))
        return column._columnIndex;
    }
    return -1;
  }

  private static bool CheckColumnCycling(CalculatedColumn column, List<int> stack, Hashtable ht)
  {
    if (column._dependColumnsIndex == null)
      return false;
    if (stack.Contains(column._columnIndex))
      return true;
    stack.Add(column._columnIndex);
    int count = column._dependColumnsIndex.Count;
    for (int index = 0; index < count; ++index)
    {
      if (ht[(object) column._dependColumnsIndex[index]] is CalculatedColumn column1 && (stack.Contains(column1._columnIndex) || CalculatedColumn.CheckColumnCycling(column1, stack, ht)))
        return true;
    }
    return false;
  }

  public void Calculate(
    DataTable recordsTable,
    CalcContext calcContext,
    IMSAttributeType[] namedValuesData,
    NamedValue[] namedValues)
  {
    if (this._tree == null)
      return;
    bool needRound = this._tree.ReturnType.Equals(typeof (double));
    DataRow[] dataRowArray = recordsTable.Select();
    int length = dataRowArray.Length;
    int count = this._tree.Variables.Count;
    object[] values = new object[count];
    for (int index1 = 0; index1 < length; ++index1)
    {
      DataRow row = dataRowArray[index1];
      for (int index2 = 0; index2 < count; ++index2)
      {
        int index3 = this._dependColumnsIndex[index2];
        if (index3 != -1)
        {
          if (calcContext != null && calcContext.IsMapped(index3))
          {
            values[index2] = (object) calcContext.GetMapValue(row[index3].ToString());
          }
          else
          {
            values[index2] = !(recordsTable.Columns[index3].DataType == typeof (string)) || row[index3] != DBNull.Value ? row[index3] : (object) string.Empty;
            if (namedValuesData != null && namedValuesData[index3] != null && namedValues != null && namedValues[index3] != null)
            {
              object obj = values[index2];
              if (obj != null)
              {
                int index4 = namedValuesData[index3].PossibleValues.IndexOf(obj);
                if (index4 != -1)
                {
                  string name = Convert.ToString(namedValuesData[index3].PossibleValuesDescriptions[index4]);
                  if (!string.IsNullOrEmpty(name))
                    values[index2] = (object) namedValues[index3].SetData(name, obj);
                }
              }
            }
          }
        }
        else
          values[index2] = (object) DBNull.Value;
      }
      try
      {
        this.AssignValue(needRound, values, row);
      }
      catch (Exception ex)
      {
        row.RowError = ex.Message;
      }
    }
  }

  private void AssignValue(bool needRound, object[] values, DataRow row)
  {
    if (this._tree == null)
      return;
    object obj = this._tree.Evaluate(values);
    if (needRound && obj != null && !DBNull.Value.Equals(obj))
    {
      if (obj.GetType().Equals(typeof (double)))
      {
        row[this._columnIndex] = (object) Math.Round(Convert.ToDouble(obj), Intermech.Consts.MaxPrecision);
      }
      else
      {
        double result;
        if (double.TryParse(obj.ToString(), out result))
        {
          row[this._columnIndex] = (object) Math.Round(result, Intermech.Consts.MaxPrecision);
        }
        else
        {
          try
          {
            if (obj is string && string.IsNullOrEmpty(Convert.ToString(obj)))
              obj = (object) DBNull.Value;
            row[this._columnIndex] = obj;
          }
          catch (Exception ex)
          {
          }
        }
      }
    }
    else
      row[this._columnIndex] = obj;
  }

  public void Calculate(
    DataRow row,
    CalcContext calcContext,
    IMSAttributeType[] namedValuesData,
    NamedValue[] namedValues)
  {
    if (this._tree == null)
      return;
    int count = this._tree.Variables.Count;
    bool needRound = this._tree.ReturnType.Equals(typeof (double));
    object[] values = new object[count];
    for (int index1 = 0; index1 < count; ++index1)
    {
      int index2 = this._dependColumnsIndex[index1];
      if (index2 != -1)
      {
        if (calcContext != null && calcContext.IsMapped(index2))
        {
          values[index1] = (object) calcContext.GetMapValue(row[index2].ToString());
        }
        else
        {
          values[index1] = !(row.Table.Columns[index2].DataType == typeof (string)) || row[index2] != DBNull.Value ? row[index2] : (object) string.Empty;
          if (namedValuesData != null && namedValuesData[index2] != null && namedValues != null && namedValues[index2] != null)
          {
            object obj = values[index1];
            if (obj != null)
            {
              int index3 = namedValuesData[index2].PossibleValues.IndexOf(obj);
              if (index3 != -1)
              {
                string name = Convert.ToString(namedValuesData[index2].PossibleValuesDescriptions[index3]);
                if (!string.IsNullOrEmpty(name))
                  values[index1] = (object) namedValues[index2].SetData(name, obj);
              }
            }
          }
        }
      }
      else
        values[index1] = (object) DBNull.Value;
    }
    try
    {
      this.AssignValue(needRound, values, row);
    }
    catch (Exception ex)
    {
      row.RowError = ex.Message;
    }
  }

  public void UpdateIndexes(DataTable dataTable)
  {
    if (this._tree == null)
      return;
    DataColumnCollection columns = dataTable.Columns;
    this._columnIndex = CalculatedColumn.GetColumnIndex(columns, this._columnName);
    int count = this._tree.Variables.Count;
    this._dependColumnsIndex = new List<int>(count);
    for (int index = 0; index < count; ++index)
      this._dependColumnsIndex.Add(CalculatedColumn.GetColumnIndex(columns, this._tree.Variables[index].Name));
  }

  private static int GetColumnIndex(DataColumnCollection columns, string name)
  {
    int count = columns.Count;
    for (int index = 0; index < count; ++index)
    {
      if (columns[index].ColumnName.Equals(name) || columns[index].Caption.Equals(name))
        return index;
    }
    return -1;
  }
}
