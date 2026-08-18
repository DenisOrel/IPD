// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Portal.PortalSettings
// Assembly: Intermech.Interfaces.Portal, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F7558C4C-BFAF-4679-9F10-E5048F615D8F
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Portal.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Portal.xml

using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Portal;

/// <summary>Настройки</summary>
public class PortalSettings
{
  /// <summary>
  /// Игнорировать фильтрацию объектов по типу узла его опубликовавших по умолчанию
  /// </summary>
  private readonly bool _defaultSitesSystemTypesIgnore;
  /// <summary>Удалять импортированные пакеты по умолчанию</summary>
  private readonly bool _defaultDeleteImportedPackets;

  /// <summary>
  /// Игнорировать фильтрацию объектов по типу узла его опубликовавших
  /// </summary>
  public bool SitesSystemTypesIgnore { get; private set; }

  /// <summary>Корневая папка хранилища файлов портала</summary>
  public string PortalFileStorage { get; private set; }

  /// <summary>Запрещенные в результатах селектов типы объектов</summary>
  public string[] DisableSelectTypes { get; private set; }

  /// <summary>Удалять импортированные пакеты</summary>
  public bool DeleteImportedPackets { get; private set; }

  public void Initialize(NameValueCollection appSettings)
  {
    PortalSettings portalSettings = new PortalSettings();
    string str1 = appSettings.Get("SitesSystemTypesIgnore");
    this.SitesSystemTypesIgnore = string.IsNullOrEmpty(str1) ? this._defaultSitesSystemTypesIgnore : str1.ToLower() == "true";
    string str2 = appSettings.Get("PortalFileStorage");
    this.PortalFileStorage = string.IsNullOrEmpty(str2) ? string.Empty : str2;
    string str3 = appSettings.Get("DisableSelectTypes");
    if (!string.IsNullOrEmpty(str3))
      this.DisableSelectTypes = str3.Split(';');
    string str4 = appSettings.Get("DeleteImportedPackets");
    this.DeleteImportedPackets = string.IsNullOrEmpty(str4) ? this._defaultDeleteImportedPackets : str4.ToLower() == "true";
  }
}
