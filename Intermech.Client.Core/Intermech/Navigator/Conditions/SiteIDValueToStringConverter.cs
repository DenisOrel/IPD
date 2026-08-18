
// Type: Intermech.Navigator.Conditions.SiteIDValueToStringConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Interfaces.WebPortal;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Conditions;

internal sealed class SiteIDValueToStringConverter : ValueToStringConverter
{
  public SiteIDValueToStringConverter()
    : base((object) SelectionParameterTypes.sptSiteID)
  {
  }

  public override string ConvertValue(
    IConditionDataProvider dataProvider,
    object value,
    object typeID)
  {
    string str1 = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ISitesCacheService customService = sessionKeeper.Session.GetCustomService<ISitesCacheService>();
      string str2 = Convert.ToString(value);
      if (str2.Length == 1)
      {
        SiteInfo site = customService.GetSite(str2[0]);
        str1 = site != null ? site.Caption : Convert.ToString(value);
      }
      else if (str2.Length > 1)
      {
        SiteInfo site = customService.GetSite(str2[0]);
        str1 = site != null ? site.Caption : Convert.ToString(str2[0]);
        str1 += ",...";
      }
    }
    return str1;
  }
}
