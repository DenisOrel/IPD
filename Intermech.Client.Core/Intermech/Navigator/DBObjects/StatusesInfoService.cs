
// Type: Intermech.Navigator.DBObjects.StatusesInfoService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;


namespace Intermech.Navigator.DBObjects;

public class StatusesInfoService : INodeStatusesInfo
{
  private IElementStatusesClientService serv;
  private Dictionary<string, StatusesServiceItem> items;
  private List<string> pluginGuids;

  public StatusesInfoService() => this.Reload();

  public void Reload()
  {
    this.serv = this.serv ?? Holder.ElementStatusesClientService;
    this.items = new Dictionary<string, StatusesServiceItem>();
    this.pluginGuids = new List<string>();
    List<Guid> guidList = new List<Guid>();
    foreach (string disabledPlugin in this.serv.DisabledPlugins)
    {
      Guid empty = Guid.Empty;
      ref Guid local = ref empty;
      if (Guid.TryParse(disabledPlugin, out local))
        guidList.Add(empty);
    }
    IPluginStatusesTable customService = (IPluginStatusesTable) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IPluginStatusesTable));
    foreach (KeyValuePair<string, ElementStatusesPluginDescription> plugin in this.serv.Plugins)
    {
      string key = plugin.Key;
      Guid pluginGuid = new Guid(key);
      if (!guidList.Contains(pluginGuid))
      {
        this.pluginGuids.Add(key);
        StatusesServiceItem statusesServiceItem = new StatusesServiceItem();
        foreach (DataRow row in (InternalDataCollectionBase) customService.GetPluginStatusesTable(key, false, (int[]) null).Rows)
        {
          int int32 = Convert.ToInt32(row[0]);
          statusesServiceItem.Add(int32, Convert.ToString(row[1]));
          statusesServiceItem.Add(int32, this.serv.GetStatusIcon(pluginGuid, int32));
        }
        this.items.Add(key, statusesServiceItem);
      }
    }
  }

  public Image[] GetIcons(INodeID nodeId, object columnValue)
  {
    List<Image> imageList = new List<Image>();
    if (columnValue != null && columnValue != DBNull.Value && columnValue.GetType() == typeof (byte[]))
    {
      byte[] bytes = (byte[]) columnValue;
      for (int index = 0; index < this.pluginGuids.Count; ++index)
      {
        foreach (int statuse in this.serv.GetStatuses(this.pluginGuids[index], bytes))
        {
          Image icon = this.items.ContainsKey(this.pluginGuids[index]) ? this.items[this.pluginGuids[index]].GetIcon(statuse) : (Image) null;
          if (icon != null)
            imageList.Add(icon);
        }
      }
    }
    return imageList.ToArray();
  }

  public string GetDescription(
    IServiceProvider services,
    INodeID nodeId,
    object columnValue,
    int iconIndex)
  {
    if (columnValue != null && columnValue != DBNull.Value && columnValue.GetType() == typeof (byte[]))
    {
      byte[] bytes = (byte[]) columnValue;
      int num = -1;
      for (int index = 0; index < this.pluginGuids.Count; ++index)
      {
        foreach (int statuse in this.serv.GetStatuses(this.pluginGuids[index], bytes))
        {
          if (this.items[this.pluginGuids[index]].GetIcon(statuse) != null)
          {
            ++num;
            if (num == iconIndex)
            {
              string empty = string.Empty;
              string str1 = string.Empty;
              if (this.pluginGuids[index] == "cad005f2-306c-11d8-b4e9-00304f19f545")
              {
                FiltrateVersionsLog service = services == null || !UISettings.ShowVersionsLog ? (FiltrateVersionsLog) null : services.GetService(typeof (FiltrateVersionsLog)) as FiltrateVersionsLog;
                NodeID nodeId1 = nodeId as NodeID;
                if (service != null && nodeId1 != null)
                {
                  FiltrateVersionsLogEntry versionsLogEntry = service[nodeId1.RelationTypeID, nodeId1.PrjLinkID, nodeId1.ObjectID];
                  if (versionsLogEntry != null)
                  {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (versionsLogEntry.MainAttribute != 0)
                    {
                      if (versionsLogEntry.Criterion >= 0)
                        stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Client.Core_1357"), (object) MetaDataHelper.GetAttributeTypeName(versionsLogEntry.MainAttribute)));
                      else
                        stringBuilder.Append(string.Format(LocalizationHolder.rm.GetString("Client.Core_1358"), (object) MetaDataHelper.GetAttributeTypeName(versionsLogEntry.MainAttribute)));
                    }
                    str1 = stringBuilder.Length > 0 ? "\n" + stringBuilder.ToString() : string.Empty;
                  }
                }
              }
              string description = this.items[this.pluginGuids[index]].GetDescription(statuse);
              string str2 = str1;
              return empty + description + str2;
            }
          }
        }
      }
    }
    return string.Empty;
  }

  /// <summary>
  /// Возвращает шрифт для указанной ячейки, если есть какие-то проблемы с её содержимым, или null
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="nodeId">Идентификатор элемента навигации</param>
  /// <param name="columnValue">Значение колонки</param>
  /// <param name="parentFont">Текущий шрифт</param>
  /// <returns>Шрифт или null, если не требуется выделение особым шрифтом</returns>
  public Font GetFont(
    IServiceProvider services,
    INodeID nodeId,
    object columnValue,
    Font parentFont)
  {
    Font font = (Font) null;
    if (columnValue != null && columnValue != DBNull.Value)
    {
      Type type = columnValue.GetType();
      if (type == typeof (byte[]))
      {
        byte[] bytes = (byte[]) columnValue;
        for (int index = 0; index < this.pluginGuids.Count; ++index)
        {
          if (this.pluginGuids[index] == "cad005f2-306c-11d8-b4e9-00304f19f545")
          {
            foreach (int statuse in this.serv.GetStatuses(this.pluginGuids[index], bytes))
            {
              if (statuse == 1)
                font = new Font(parentFont, FontStyle.Strikeout);
            }
          }
        }
      }
      else if (type == typeof (ObjectFiltrationState))
      {
        if ((ObjectFiltrationState) columnValue == ObjectFiltrationState.fsCompositeVersionNotFound)
          font = new Font(parentFont, FontStyle.Strikeout);
        else if (nodeId is NodeID && (nodeId as NodeID).fontStyle != FontStyle.Regular)
          font = new Font(parentFont, (nodeId as NodeID).fontStyle);
      }
    }
    return font;
  }
}
