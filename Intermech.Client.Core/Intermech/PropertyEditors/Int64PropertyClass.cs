
// Type: Intermech.PropertyEditors.Int64PropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.Data;


namespace Intermech.PropertyEditors;

[Serializable]
public class Int64PropertyClass : PropertyClass
{
  public Int64PropertyClass()
    : this(0L)
  {
    this.value = (object) null;
  }

  public Int64PropertyClass(long aInt64)
    : this(aInt64, string.Empty, (DataTable) null)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aInt64"></param>
  /// <param name="aDescription">описатель к значению. если String.Empty, то искать самостоятельно из списка, указанного далее</param>
  /// <param name="aPossibleValuesDataTable">опциональный список допустимых значений, если есть Description, то будут выдаваться функцией ToString</param>
  public Int64PropertyClass(long aInt64, string aDescription, DataTable aPossibleValuesDataTable)
  {
    this.value = (object) aInt64;
    this.description = aDescription;
    this.possibleValuesDataTable = aPossibleValuesDataTable;
  }

  public override string ToString()
  {
    if (this.value == null)
      return string.Empty;
    if (this.description != string.Empty)
      return this.description;
    if (this.possibleValuesDataTable != null)
    {
      DataTable possibleValuesDataTable = this.possibleValuesDataTable;
      DataRow[] dataRowArray = possibleValuesDataTable.Select($"{ClientCommons.ExtractValueFieldName(possibleValuesDataTable)}={this.value.ToString()}");
      if (dataRowArray != null && dataRowArray.Length != 0 && Convert.ToString(dataRowArray[0]["F_DESCRIPTION"]) != string.Empty)
        return Convert.ToString(dataRowArray[0]["F_DESCRIPTION"]);
    }
    return this.value.ToString();
  }
}
