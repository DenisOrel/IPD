// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.GridFilterBase
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public abstract class GridFilterBase : IGridFilter, IDisposable
{
  private bool _useCustomFilterPlacement;
  private bool _locked;

  protected GridFilterBase(bool useCustomFilterPlacement)
  {
    this._useCustomFilterPlacement = useCustomFilterPlacement;
    this._locked = false;
  }

  public event EventHandler Changed;

  public abstract Control FilterControl { get; }

  public abstract bool ApplyAutoComplete(DataColumn column);

  public bool UseCustomFilterPlacement
  {
    get => this._useCustomFilterPlacement;
    set => this._useCustomFilterPlacement = value;
  }

  public abstract bool HasFilter { get; }

  public abstract ComboBox ComboBox { get; }

  public abstract string GetFilterText(string columnName);

  public abstract ConditionItem GetFilter(string columnName);

  public abstract void SetFilter(ConditionItem filter);

  public abstract void Clear();

  public void Lock() => this._locked = true;

  public void UnLock() => this._locked = false;

  protected ConditionItem NewConditionItem(Condition cond, string data)
  {
    return this.NewConditionItem(cond, data, string.Empty);
  }

  protected ConditionItem NewConditionItem(Condition cond, string data, string data2)
  {
    return new ConditionItem()
    {
      Condition = cond,
      Data = data,
      Data2 = data2
    };
  }

  protected void OnChanged()
  {
    if (this._locked)
      return;
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  protected bool ApplyAutoComplete(DataColumn column, TextBox textBox)
  {
    if (textBox != null)
    {
      textBox.AutoCompleteCustomSource = this.GetAutocompleteData(column);
      if (textBox.AutoCompleteCustomSource != null)
      {
        textBox.AutoCompleteMode = AutoCompleteMode.Suggest;
        textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        return true;
      }
    }
    return false;
  }

  protected bool ApplyAutoComplete(DataColumn column, ComboBox comboBox)
  {
    if (comboBox != null)
    {
      List<string> distinctValues = GridFilterBase.GetDistinctValues(column, out System.Type _, out bool _, out bool _);
      if (distinctValues != null && distinctValues.Count > 0)
      {
        comboBox.DataSource = (object) null;
        comboBox.Items.Clear();
        comboBox.Items.AddRange((object[]) distinctValues.ToArray());
        return true;
      }
    }
    return false;
  }

  private AutoCompleteStringCollection GetAutocompleteData(DataColumn col)
  {
    AutoCompleteStringCollection autocompleteData = (AutoCompleteStringCollection) null;
    List<string> distinctValues = GridFilterBase.GetDistinctValues(col, out System.Type _, out bool _, out bool _);
    if (distinctValues != null && distinctValues.Count > 0)
    {
      autocompleteData = new AutoCompleteStringCollection();
      autocompleteData.AddRange(distinctValues.ToArray());
    }
    return autocompleteData;
  }

  public static List<string> GetDistinctValues(
    DataColumn col,
    out System.Type dataType,
    out bool isArray,
    out bool containsDbNull)
  {
    dataType = col.DataType;
    isArray = false;
    containsDbNull = false;
    if (dataType.Equals(typeof (ValuesArray)))
    {
      dataType = col.ExtendedProperties[(object) nameof (dataType)] as System.Type;
      isArray = true;
    }
    List<string> distinctValues = (List<string>) null;
    bool flag = false;
    if (col != null)
    {
      distinctValues = new List<string>();
      DataView defaultView = col.Table.DefaultView;
      int count = defaultView.Count;
      for (int recordIndex = 0; recordIndex < count; ++recordIndex)
      {
        DataRow row = defaultView[recordIndex].Row;
        object obj1 = row[col];
        if (obj1 == null || DBNull.Value.Equals(obj1) || string.IsNullOrEmpty(Convert.ToString(obj1)))
          containsDbNull = true;
        else if (row[col] is ValuesArray valuesArray)
        {
          foreach (object obj2 in valuesArray.GetArray())
          {
            if (TableLoadHelper.IsNull(obj2))
            {
              flag = true;
            }
            else
            {
              string str = obj2.ToString();
              if (str.Length > 0)
              {
                int num = distinctValues.BinarySearch(str);
                if (num < 0)
                  distinctValues.Insert(~num, str);
              }
              else
                flag = true;
            }
          }
        }
        else
        {
          string str = row[col].ToString();
          if (str.Length > 0)
          {
            int num = distinctValues.BinarySearch(str);
            if (num < 0)
              distinctValues.Insert(~num, str);
          }
          else
            flag = true;
        }
      }
      if (distinctValues.Count < 1 & flag)
        return (List<string>) null;
      if (dataType.Equals(typeof (double)))
        distinctValues.Sort((Comparison<string>) ((x, y) =>
        {
          if (x.Length == 0)
            return -1;
          if (y.Length == 0)
            return 1;
          double result1;
          double.TryParse(x, out result1);
          double result2;
          double.TryParse(y, out result2);
          return result1.CompareTo(result2);
        }));
      if (dataType.Equals(typeof (long)))
        distinctValues.Sort((Comparison<string>) ((x, y) =>
        {
          if (x.Length == 0)
            return -1;
          if (y.Length == 0)
            return 1;
          long result3;
          long.TryParse(x, out result3);
          long result4;
          long.TryParse(y, out result4);
          return result3.CompareTo(result4);
        }));
      else if (col.DataType.Equals(typeof (int)))
        distinctValues.Sort((Comparison<string>) ((x, y) =>
        {
          if (x.Length == 0)
            return -1;
          if (y.Length == 0)
            return 1;
          int result5;
          int.TryParse(x, out result5);
          int result6;
          int.TryParse(y, out result6);
          return result5.CompareTo(result6);
        }));
      else
        distinctValues.Sort((IComparer<string>) StringComparer.InvariantCulture);
      if (containsDbNull)
        distinctValues.Insert(0, SpecialValue.NullValue.ToString());
      distinctValues.Insert(0, string.Empty);
    }
    return distinctValues;
  }

  protected void CalcDropDownWidth(ComboBox comboBox)
  {
    int num1 = 0;
    Font font = comboBox.Font;
    foreach (object obj in comboBox.Items)
    {
      if (obj != null)
      {
        int width = TextRenderer.MeasureText(obj.ToString(), font).Width;
        if (width > num1)
          num1 = width;
      }
    }
    int num2 = num1 + 2;
    if (comboBox.Items.Count > comboBox.MaxDropDownItems)
      num2 += SystemInformation.VerticalScrollBarWidth;
    if (num2 > 400)
      num2 = 400;
    if (comboBox.DropDownWidth >= num2)
      return;
    comboBox.DropDownWidth = num2;
  }

  protected string EscapeString(string value)
  {
    char[] anyOf = new char[4]{ '%', '*', '[', ']' };
    if (string.IsNullOrEmpty(value) || value.LastIndexOfAny(anyOf) < 0)
      return value;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (char ch in value.ToCharArray())
    {
      switch (ch)
      {
        case '%':
        case '*':
        case '[':
        case ']':
          stringBuilder.Append('[');
          stringBuilder.Append(ch);
          stringBuilder.Append(']');
          break;
        default:
          stringBuilder.Append(ch);
          break;
      }
    }
    return stringBuilder.ToString();
  }

  public virtual void Dispose()
  {
  }
}
