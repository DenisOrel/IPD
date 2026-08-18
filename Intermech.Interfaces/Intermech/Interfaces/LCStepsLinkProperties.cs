
// Type: Intermech.Interfaces.LCStepsLinkProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Структура, описывающая свойства связи между шагами ЖЦ</summary>
    [Serializable]
    public struct LCStepsLinkProperties(
      int fromStepID,
      int toStepID,
      string note,
      int routeId,
      int lParams)
    {
      public int FromStepID = fromStepID;
      public int ToStepID = toStepID;
      public string Note = note;
      public int RouteID = routeId;
      public int Params = lParams;

      public LCStepsLinkProperties(DataRow dataRow)
        : this(Convert.ToInt32(dataRow["F_FROM_STEP"]), Convert.ToInt32(dataRow["F_TO_STEP"]), dataRow["F_NOTE"].ToString(), dataRow["F_ROUTE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataRow["F_ROUTE_ID"]), dataRow["F_PARAMS"] == DBNull.Value ? 0 : Convert.ToInt32(dataRow["F_PARAMS"]))
      {
      }

      public static void StoreToDataRow(LCStepsLinkProperties linkProps, DataRow dataRow)
      {
        dataRow["F_FROM_STEP"] = (object) linkProps.FromStepID;
        dataRow["F_TO_STEP"] = (object) linkProps.ToStepID;
        dataRow["F_NOTE"] = (object) linkProps.Note;
        dataRow["F_ROUTE_ID"] = (object) linkProps.RouteID;
        dataRow["F_PARAMS"] = (object) linkProps.Params;
      }
    }
}
