
// Type: Intermech.Search.Configuration.ConfigurationPageHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.Configuration;

public static class ConfigurationPageHelper
{
  public static void CreateAndRegisterPages()
  {
    IPropertyPagesService propertyPagesService = ServiceLocator.Get<IPropertyPagesService>();
    IConfigurationOptionInfoProvider optionInfoProvider = ServiceLocator.Get<IConfigurationOptionInfoProvider>();
    ICurrentUserAndRole currentUserAndRole = ServiceLocator.Get<ICurrentUserAndRole>();
    foreach (KeyValuePair<string, List<ConfigurationOptionInfo>> keyValuePair in ConfigurationPageHelper.GroupOptionsByPage(optionInfoProvider.GetOptionsInfo().Where<ConfigurationOptionInfo>((Func<ConfigurationOptionInfo, bool>) (o => !string.IsNullOrEmpty(o.Page)))))
    {
      string[] pathAndNameParts = ConfigurationPageHelper.GetPagePathAndNameParts(keyValuePair.Key);
      string path = pathAndNameParts[0];
      string pageName = pathAndNameParts[1];
      List<ConfigurationOptionInfo> list = keyValuePair.Value.Where<ConfigurationOptionInfo>((Func<ConfigurationOptionInfo, bool>) (o =>
      {
        if (!o.CheckAdmin)
          return true;
        return o.CheckAdmin && currentUserAndRole.IsAdmin;
      })).ToList<ConfigurationOptionInfo>();
      if (list.Count > 0)
      {
        ConfigurationPage page = new ConfigurationPage(pageName, list);
        propertyPagesService.AddPage(path, (IPropertyPage) page);
      }
    }
  }

  private static Dictionary<string, List<ConfigurationOptionInfo>> GroupOptionsByPage(
    IEnumerable<ConfigurationOptionInfo> optionsInfo)
  {
    Dictionary<string, List<ConfigurationOptionInfo>> dictionary = new Dictionary<string, List<ConfigurationOptionInfo>>();
    foreach (ConfigurationOptionInfo configurationOptionInfo in optionsInfo)
    {
      List<ConfigurationOptionInfo> configurationOptionInfoList = new List<ConfigurationOptionInfo>();
      dictionary.TryGetValue(configurationOptionInfo.Page, out configurationOptionInfoList);
      if (configurationOptionInfoList == null)
      {
        configurationOptionInfoList = new List<ConfigurationOptionInfo>();
        dictionary.Add(configurationOptionInfo.Page, configurationOptionInfoList);
      }
      configurationOptionInfoList.Add(configurationOptionInfo);
    }
    return dictionary;
  }

  private static string[] GetPagePathAndNameParts(string pagePathAndName)
  {
    string[] source = pagePathAndName.Split('/');
    return new string[2]
    {
      string.Join("\\", source),
      ((IEnumerable<string>) source).Last<string>()
    };
  }
}
