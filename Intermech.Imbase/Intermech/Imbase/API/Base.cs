// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.Base
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.Imbase.API;

internal class Base
{
  private DataTable _dataTable;
  private DataView _dataView;
  private DataRow _dataRow;
  private FieldInfo[] _fieldsInfo;
  private ContextInfo _context;

  internal Base(DataTable dataTable, FieldInfo[] fieldsInfo, ContextInfo context)
    : this(dataTable, fieldsInfo)
  {
    this._context = context;
  }

  internal Base(DataTable dataTable, FieldInfo[] fieldsInfo)
  {
    this._dataTable = dataTable;
    this._fieldsInfo = fieldsInfo;
    this._dataView = new DataView(this._dataTable);
    this.First();
  }

  private void First() => this.SetRow(0);

  internal int records() => this._dataView.Count;

  internal RecordItem Record(int recNo, int fieldNo)
  {
    this.SetRow(recNo);
    return new RecordItem(this._dataRow[this._fieldsInfo[fieldNo].AttributeId.ToString()].ToString());
  }

  internal void SetRow(int recNo) => this._dataRow = this._dataView[recNo].Row;

  internal ContextInfo Context => this._context;

  internal long CurrentKey
  {
    get => Convert.ToInt64(this._dataRow["-2"]);
    set
    {
      if (this.CurrentKey == value)
        return;
      int count = this._dataView.Count;
      for (int recordIndex = 0; recordIndex < count; ++recordIndex)
      {
        if (Convert.ToInt64(this._dataView[recordIndex]["-2"]).Equals(value))
        {
          this._dataRow = this._dataView[recordIndex].Row;
          break;
        }
      }
    }
  }

  internal void Close()
  {
    this._dataRow = (DataRow) null;
    this._dataTable = (DataTable) null;
    this._dataView = (DataView) null;
  }

  internal int FieldsFount => this._fieldsInfo == null ? 0 : this._fieldsInfo.Length;

  internal FieldInfo GetFieldInfo(int index) => this._fieldsInfo[index];

  internal string ValueById(int attId)
  {
    int length = this._fieldsInfo.Length;
    for (int index = 0; index < length; ++index)
    {
      if (this._fieldsInfo[index].AttributeId == attId)
      {
        string str = Convert.ToString(this._dataRow[attId.ToString()]);
        if (this._fieldsInfo[index].FieldType == FieldType.Float)
          str = str.Replace(',', '.');
        return str;
      }
    }
    return string.Empty;
  }

  internal int GetFieldByShortName(string shortName)
  {
    int length = this._fieldsInfo.Length;
    for (int fieldByShortName = 0; fieldByShortName < length; ++fieldByShortName)
    {
      if (this._fieldsInfo[fieldByShortName].ShortName.Equals(shortName, StringComparison.InvariantCultureIgnoreCase))
        return fieldByShortName;
    }
    return -1;
  }

  internal void GetShortList(List<string> list, bool onlyShort)
  {
    list.Clear();
    int length = this._fieldsInfo.Length;
    for (int index = 0; index < length; ++index)
    {
      FieldInfo fieldInfo = this._fieldsInfo[index];
      if (fieldInfo.AttributeId > 0)
      {
        string str1 = fieldInfo.ShortName;
        if (str1.Length == 0)
        {
          if (!onlyShort)
            str1 = fieldInfo.LongName;
          else
            continue;
        }
        object obj = this._dataRow[fieldInfo.AttributeId.ToString()];
        string str2 = !(obj.GetType() == typeof (string)) ? (fieldInfo.FieldType != FieldType.Float ? obj.ToString() : obj.ToString().Replace(',', '.')) : $"\"{obj.ToString().Replace("\"", "\"\"")}\"";
        list.Add($"{str1}={str2}");
      }
    }
  }

  internal void SortBy(int fieldNo)
  {
    this._dataView.Sort = $"[{this.GetFieldInfo(fieldNo).AttributeId.ToString()}]";
    this.First();
  }

  internal void SortBy(int[] fieldNo)
  {
    int length = fieldNo.Length;
    StringBuilder stringBuilder = new StringBuilder(128 /*0x80*/);
    for (int index = 0; index < length; ++index)
    {
      FieldInfo fieldInfo = this.GetFieldInfo(fieldNo[index]);
      if (stringBuilder.Length > 0)
        stringBuilder.Append(',');
      stringBuilder.Append($"[{fieldInfo.AttributeId}]");
    }
    this._dataView.Sort = stringBuilder.ToString();
    this.First();
  }
}
