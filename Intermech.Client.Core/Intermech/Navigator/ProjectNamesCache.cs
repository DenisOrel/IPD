
// Type: Intermech.Navigator.ProjectNamesCache
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Кэш названий проектов. При отсутствии в нем имени для указанного идентификатора кэш лезет в базу.
/// </summary>
public class ProjectNamesCache : ICache, IProjectNamesCache
{
  /// <summary>
  /// Коллекция пар значений [(Int64)Идентификатор проекта] = [(string)Название проекта]
  /// </summary>
  private Dictionary<long, string> _names = new Dictionary<long, string>();

  /// <summary>Сбросить содержимое кэша</summary>
  public void Reset() => this._names.Clear();

  /// <summary>Вернуть название проекта по его идентификатору</summary>
  /// <param name="projectObjectID">Идентификатор проекта</param>
  /// <returns>Название проекта</returns>
  public string GetProjectName(long projectObjectID)
  {
    if (projectObjectID == 0L)
      return "";
    if (!this._names.ContainsKey(projectObjectID))
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(projectObjectID);
          this._names[projectObjectID] = objectInfo.Caption;
        }
      }
      catch
      {
        this._names[projectObjectID] = LocalizationHolder.rm.GetString("Client.Core_267") + projectObjectID.ToString();
      }
    }
    return this._names[projectObjectID];
  }
}
