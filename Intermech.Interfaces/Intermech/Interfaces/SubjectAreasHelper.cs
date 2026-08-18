
// Type: Intermech.Interfaces.SubjectAreasHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Хелпер для работы с предметными областями</summary>
    public class SubjectAreasHelper
    {
      public static string GetAreasCaption(DataTable areasTable, string areas)
      {
        string areasCaption = areas.Trim();
        if (areasCaption == "")
          return "Все предметные области";
        foreach (DataRow row in (InternalDataCollectionBase) areasTable.Rows)
        {
          if (areasCaption.IndexOf(Convert.ToChar(row["F_AREA_ID"])) > -1)
            areasCaption = $"{areasCaption}{row["F_AREA_NAME"].ToString()}, ";
        }
        if (areasCaption != "")
          areasCaption = areasCaption.Substring(1, areasCaption.Length - 2);
        return areasCaption;
      }
    }
}
