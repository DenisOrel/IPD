// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchiveColumnsSettingsCacheService
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Archives;

internal class ArchiveColumnsSettingsCacheService : IArchiveColumnsSettingsCacheService
{
  private List<ArchiveColumnsSettings> _archiveColumnsSettingsCache = new List<ArchiveColumnsSettings>();

  /// <summary>
  /// Поиск настройки происходит только для переданного в метод архива
  /// Это связано с тем, что метод используется в условно-рекурсивном методе FindParentCategoryType в DocumsObject
  /// Наличие сохраненных значений для архива там проверяется на каждом уровне дерева архивов
  /// </summary>
  /// <param name="archiveId"></param>
  /// <param name="userRoleId"></param>
  /// <returns></returns>
  [CanBeNull]
  public NodeColumnCollection GetArchiveColumnsSettingsForRole(long archiveId, long userRoleId)
  {
    ArchiveColumnsSettings archiveColumnsSettings1 = this._archiveColumnsSettingsCache.FirstOrDefault<ArchiveColumnsSettings>((Func<ArchiveColumnsSettings, bool>) (x => x.ArchiveID == archiveId));
    if (archiveColumnsSettings1 != null)
      return this.GetColumnsCollectionForRole(archiveColumnsSettings1.RolesColumnSettings, userRoleId);
    ArchiveColumnsSettings archiveColumnsSettings2 = this.LoadSettingsFromBase(archiveId);
    this._archiveColumnsSettingsCache.Add(archiveColumnsSettings2);
    return this.GetColumnsCollectionForRole(archiveColumnsSettings2.RolesColumnSettings, userRoleId);
  }

  /// <summary>Получаем настройки на архив</summary>
  /// <param name="archiveId">ИД архива</param>
  /// <returns></returns>
  [NotNull]
  public ArchiveColumnsSettings GetArchiveColumnsSettings(long archiveId)
  {
    ArchiveColumnsSettings archiveColumnsSettings1 = this._archiveColumnsSettingsCache.FirstOrDefault<ArchiveColumnsSettings>((Func<ArchiveColumnsSettings, bool>) (x => x.ArchiveID == archiveId));
    if (archiveColumnsSettings1 != null)
      return archiveColumnsSettings1;
    ArchiveColumnsSettings archiveColumnsSettings2 = this.LoadSettingsFromBase(archiveId);
    this._archiveColumnsSettingsCache.Add(archiveColumnsSettings2);
    return archiveColumnsSettings2;
  }

  /// <summary>
  /// Ищет настройки по умолчанию для роли или дефолтные, если для роли нет
  /// </summary>
  /// <param name="rolesColumnSettings"></param>
  /// <param name="userRoleId"></param>
  /// <returns>Настройки по умолчанию для роли или дефолтные, если для роли нет. Null - если нет ни того, ни другого.</returns>
  [CanBeNull]
  private NodeColumnCollection GetColumnsCollectionForRole(
    List<RolesColumnsSettings> rolesColumnSettings,
    long userRoleId)
  {
    RolesColumnsSettings rolesColumnsSettings = rolesColumnSettings.FirstOrDefault<RolesColumnsSettings>((Func<RolesColumnsSettings, bool>) (x => x.RoleID == userRoleId));
    if (rolesColumnsSettings != null)
      return rolesColumnsSettings.Columns;
    return rolesColumnSettings.FirstOrDefault<RolesColumnsSettings>((Func<RolesColumnsSettings, bool>) (x => x.RoleID == Consts.DefaultRoleId))?.Columns;
  }

  /// <summary>Зачитываем настройки колонок архива из базы</summary>
  /// <param name="archiveId">ИД архива</param>
  /// <returns>Настройки колонок архива по умолчанию</returns>
  /// 
  ///             Пустой список внутри, если для архива нет настроек
  [NotNull]
  public ArchiveColumnsSettings LoadSettingsFromBase(long archiveId)
  {
    ArchiveColumnsSettings archiveColumnsSettings = new ArchiveColumnsSettings()
    {
      ArchiveID = archiveId
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(archiveId, Consts.ColumnsSettingsAttrGuid);
      if (objectAttributeByGuid == null)
        return archiveColumnsSettings;
      try
      {
        using (MemoryStream memoryStream = new MemoryStream())
        {
          new BlobProcReader(objectAttributeByGuid, 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(sessionKeeper.Session);
          if (memoryStream.Length > 0L)
          {
            memoryStream.Position = 0L;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load((Stream) memoryStream);
            archiveColumnsSettings.LoadSettingsFromXmlDoc(xmlDoc);
          }
        }
      }
      catch (Exception ex)
      {
        IEventLog eventLog = sessionKeeper.Session.EventLog;
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(archiveColumnsSettings.ArchiveID);
        string EventStr = $"{string.Format(ServiceHolder.rm.GetString("Archives_215"), (object) objectInfo.Caption, (object) objectInfo.ObjectID)}{Environment.NewLine} {ex.Message}{Environment.NewLine}{ex.StackTrace}";
        string empty = string.Empty;
        eventLog.AddToTrace(EventStr, 0, empty);
      }
    }
    return archiveColumnsSettings;
  }

  /// <summary>Сохранить настройки в кэше и базе</summary>
  /// <param name="archiveColumnsSettings">Настройки колонок на архив</param>
  public void SaveSettingsToCacheAndBase(ArchiveColumnsSettings archiveColumnsSettings)
  {
    this._archiveColumnsSettingsCache.Remove(this._archiveColumnsSettingsCache.FirstOrDefault<ArchiveColumnsSettings>((Func<ArchiveColumnsSettings, bool>) (x => x.ArchiveID == archiveColumnsSettings.ArchiveID)));
    this._archiveColumnsSettingsCache.Add(archiveColumnsSettings);
    this.SaveSettingsToBase(archiveColumnsSettings);
  }

  /// <summary>Сохранение настроек колонок по умолчанию в базу</summary>
  /// <param name="archiveColumnsSettings"></param>
  public void SaveSettingsToBase(ArchiveColumnsSettings archiveColumnsSettings)
  {
    XmlDocument xmlDoc = new XmlDocument();
    archiveColumnsSettings.SaveToXmlDoc(xmlDoc);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(archiveColumnsSettings.ArchiveID, Consts.ColumnsSettingsAttrGuid);
      if (objectAttributeByGuid == null)
        return;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (XmlTextWriter w = new XmlTextWriter((Stream) memoryStream, Encoding.UTF8))
        {
          w.WriteStartDocument();
          xmlDoc.WriteTo((XmlWriter) w);
          w.WriteEndDocument();
          w.Flush();
          BlobInformation aBlobInformation = new BlobInformation(memoryStream.Length, 0L, DateTime.Now, "ArchiveColumnsSettings.xml", ArcMethods.ZLibPacked, string.Empty);
          new BlobProcWriter(objectAttributeByGuid, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
        }
      }
    }
  }
}
