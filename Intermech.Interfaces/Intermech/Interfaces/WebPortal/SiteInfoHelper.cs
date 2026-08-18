
// Type: Intermech.Interfaces.WebPortal.SiteInfoHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces.WebPortal
{
    public static class SiteInfoHelper
    {
      /// <summary>
      /// Получить список узлов из БД без фильтрации по типу системы
      /// </summary>
      /// <param name="session"></param>
      /// <returns></returns>
      public static SiteInfo[] GetSitesFromDB(IUserSession session)
      {
        return SiteInfoHelper.GetSitesFromDB(session, SystemTypes.Unknown);
      }

      /// <summary>Получить список узлов из БД</summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <param name="filterType">Фильтр по типу системы</param>
      /// <returns></returns>
      public static SiteInfo[] GetSitesFromDB(IUserSession session, SystemTypes filterType)
      {
        int attributeId = session.GetAttributeType(PortalConsts.attributeSystem, true).AttributeID;
        ConditionStructure[] conditions = (ConditionStructure[]) null;
        if (filterType != SystemTypes.Unknown)
          conditions = new ConditionStructure[1]
          {
            new ConditionStructure(attributeId, RelationalOperators.Equal, (object) (int) filterType, LogicalOperators.AND, 0, false)
          };
        DataTable dataTable = session.GetObjectCollection(PortalConsts.objtypeSites).Select(new DBRecordSetParams(conditions, new object[5]
        {
          (object) -2,
          (object) -12,
          (object) session.GetAttributeType(PortalConsts.attributeSiteCode, true).AttributeID,
          (object) -50,
          (object) attributeId
        }));
        List<SiteInfo> siteInfoList = new List<SiteInfo>(dataTable.Rows.Count);
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          if (dataTable.Rows[index][2] != DBNull.Value)
          {
            SiteInfo siteInfo = new SiteInfo(Convert.ToInt64(dataTable.Rows[index][0]), new Guid(Convert.ToString(dataTable.Rows[index][1])), Convert.ToChar(dataTable.Rows[index][2]), Convert.ToString(dataTable.Rows[index][3]), (SystemTypes) Convert.ToInt32(dataTable.Rows[index][4]));
            siteInfoList.Add(siteInfo);
          }
        }
        return siteInfoList.ToArray();
      }
    }
}
