
// Type: Intermech.PropertyEditors.DoublePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.Data;
using System.Globalization;


namespace Intermech.PropertyEditors;

[Serializable]
public class DoublePropertyClass : PropertyClass
{
  public DoublePropertyClass()
    : this(0.0)
  {
    this.value = (object) null;
  }

  public DoublePropertyClass(double aDouble)
    : this(aDouble, string.Empty, (DataTable) null)
  {
  }

  public DoublePropertyClass(
    double aDouble,
    string aDescription,
    DataTable aPossibleValuesDataTable)
  {
    this.value = (object) aDouble;
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
      DataRow[] dataRowArray = possibleValuesDataTable.Select($"{ClientCommons.ExtractValueFieldName(possibleValuesDataTable)}={Convert.ToString(this.value, (IFormatProvider) CultureInfo.InvariantCulture)}");
      if (dataRowArray != null && dataRowArray.Length != 0 && Convert.ToString(dataRowArray[0]["F_DESCRIPTION"]) != string.Empty)
        return Convert.ToString(dataRowArray[0]["F_DESCRIPTION"]);
    }
    return this.value.ToString();
  }
}
