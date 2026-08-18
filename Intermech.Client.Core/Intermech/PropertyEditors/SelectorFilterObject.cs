
// Type: Intermech.PropertyEditors.SelectorFilterObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Data;


namespace Intermech.PropertyEditors;

internal class SelectorFilterObject : ISelectorFilter
{
  private DataTable dt;

  public SelectorFilterObject(DataTable adt) => this.dt = adt;

  public bool IsInFilter(int category, object id)
  {
    switch (category)
    {
      case 3:
        if (id != null && (int) id >= 0)
        {
          DataRow[] dataRowArray = this.dt.Select("F_ATTRIBUTE_ID=" + id.ToString());
          bool flag = dataRowArray != null && dataRowArray.Length != 0;
          if (flag)
          {
            FieldTypes int32 = (FieldTypes) Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_TYPE"]);
            flag = flag && int32 != FieldTypes.ftBlob && int32 != FieldTypes.ftFile && int32 != FieldTypes.ftShortBlob;
          }
          return flag;
        }
        break;
      case 12:
        return id != null && Convert.ToInt32(id) == -1;
    }
    return false;
  }
}
