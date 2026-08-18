
// Type: Intermech.PropertyEditors.GuidPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.Data;


namespace Intermech.PropertyEditors;

[Serializable]
public class GuidPropertyClass : PropertyClass
{
  public GuidPropertyClass()
    : this(Guid.Empty)
  {
    this.value = (object) null;
  }

  public GuidPropertyClass(Guid aGuid)
    : this(aGuid, string.Empty, (DataTable) null)
  {
  }

  public GuidPropertyClass(Guid aGuid, string aDescription, DataTable aPossibleValuesDataTable)
  {
    this.value = (object) aGuid;
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
      DataRow[] dataRowArray = possibleValuesDataTable.Select($"{ClientCommons.ExtractValueFieldName(possibleValuesDataTable)}='{((Guid) this.value).ToString()}'");
      if (dataRowArray != null && dataRowArray.Length != 0 && Convert.ToString(dataRowArray[0]["F_DESCRIPTION"]) != string.Empty)
        return Convert.ToString(dataRowArray[0]["F_DESCRIPTION"]);
    }
    return this.value.ToString();
  }
}
