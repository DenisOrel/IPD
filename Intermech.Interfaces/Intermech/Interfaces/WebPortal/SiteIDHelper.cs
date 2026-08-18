
// Type: Intermech.Interfaces.WebPortal.SiteIDHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Text;


namespace Intermech.Interfaces.WebPortal
{
    public class SiteIDHelper
    {
      /// <summary>
      /// Получить расшифровку кодов узлов в виде строки с названиями узлов через запятую
      /// </summary>
      public static string GetCaption(ISitesCacheService service, string sitesCodes)
      {
        if (!(sitesCodes != string.Empty))
          return string.Empty;
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < sitesCodes.Length; ++index)
        {
          if (index > 0)
            stringBuilder.Append(", ");
          if (sitesCodes[index].Equals(Consts.NoSymbol))
          {
            stringBuilder.Append("<Не назначен>");
          }
          else
          {
            SiteInfo site = service.GetSite(sitesCodes[index]);
            stringBuilder.Append(site != null ? site.Caption : sitesCodes[index].ToString());
          }
        }
        return stringBuilder.ToString();
      }

      /// <summary>
      /// Функция по SITE_ID объекта определяет чужой объект для текущего узла
      /// </summary>
      public static bool IsForeign(ISitesCacheService service, string siteID)
      {
        if (service == null)
          return true;
        return service.Info == null ? !service.IsPortal : !SiteIDHelper.IsOwner(service.Info.Code, siteID);
      }

      /// <summary>
      /// Функция по SITE_ID объекта определяет чужой состав объекта для текущего узла
      /// </summary>
      public static bool IsCompositionForeign(ISitesCacheService service, string siteID)
      {
        if (string.IsNullOrEmpty(siteID))
          return false;
        if (service == null)
          return true;
        if (service.Info == null)
          return !service.IsPortal;
        if (siteID.Length == 1)
          return true;
        return siteID.Length == 2 ? !siteID[1].Equals(service.Info.Code) : !siteID[2].Equals(service.Info.Code);
      }

      /// <summary>
      /// Функция определяет, является ли текущий узел владельцем объекта
      /// </summary>
      public static bool IsOwner(char currentSiteCode, string siteID)
      {
        if (string.IsNullOrEmpty(siteID))
          return true;
        return siteID.Length >= 2 && siteID[1].Equals(currentSiteCode);
      }
    }
}
