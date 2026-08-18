
// Type: Intermech.Navigator.EventLog.Services
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Cache;
using Intermech.Cache.Storages;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.EventLog;

public sealed class Services
{
  /// <summary>
  /// Кэш, содержащий отображение идентификаторов категорий журнала событий в
  /// строковые названия категорий.
  /// </summary>
  private static readonly ICacheManager eventCategories = (ICacheManager) new Intermech.Cache.CacheManager((IStorage) new InMemoryStorage());
  private static readonly ICacheManager auditTypes = (ICacheManager) new Intermech.Cache.CacheManager((IStorage) new InMemoryStorage());
  private static readonly ICacheManager eventTypes = (ICacheManager) new Intermech.Cache.CacheManager((IStorage) new InMemoryStorage());

  static Services()
  {
    Services.UpdateEventCategories();
    Services.UpdateAuditTypes();
    Services.UpdateEventTypes();
  }

  internal static void Start()
  {
    Holder.ColumnSchemes.Register(Consts.ColumnSchemeGuid, (INodeColumnScheme) new ColumnScheme());
  }

  internal static void Stop()
  {
  }

  public static ICacheManager EventCategories => Services.eventCategories;

  public static ICacheManager AuditTypes => Services.auditTypes;

  public static ICacheManager EventTypes => Services.eventTypes;

  private static void UpdateEventCategories()
  {
    Services.eventCategories.Flush();
    Services.eventCategories.Add((object) 3, (object) LocalizationHolder.rm.GetString("Client.Core_1105"));
    Services.eventCategories.Add((object) 3, (object) LocalizationHolder.rm.GetString("Client.Core_1106"));
    Services.eventCategories.Add((object) 12, (object) LocalizationHolder.rm.GetString("Client.Core_1107"));
    Services.eventCategories.Add((object) 10, (object) LocalizationHolder.rm.GetString("Client.Core_1108"));
    Services.eventCategories.Add((object) 9, (object) LocalizationHolder.rm.GetString("Client.Core_1109"));
    Services.eventCategories.Add((object) 8, (object) LocalizationHolder.rm.GetString("Client.Core_1110"));
    Services.eventCategories.Add((object) 7, (object) LocalizationHolder.rm.GetString("Client.Core_1111"));
    Services.eventCategories.Add((object) 2, (object) LocalizationHolder.rm.GetString("Client.Core_1112"));
    Services.eventCategories.Add((object) 4, (object) LocalizationHolder.rm.GetString("Client.Core_1113"));
    Services.eventCategories.Add((object) 1, (object) LocalizationHolder.rm.GetString("Client.Core_1114"));
    Services.eventCategories.Add((object) 5, (object) LocalizationHolder.rm.GetString("Client.Core_1115"));
    Services.eventCategories.Add((object) 6, (object) LocalizationHolder.rm.GetString("Client.Core_1116"));
    Services.eventCategories.Add((object) 11, (object) LocalizationHolder.rm.GetString("Client.Core_1117"));
    Services.eventCategories.Add((object) 14, (object) LocalizationHolder.rm.GetString("Client.Core_1118"));
    Services.eventCategories.Add((object) 0, (object) LocalizationHolder.rm.GetString("Client.Core_1119"));
    Services.eventCategories.Add((object) 23, (object) LocalizationHolder.rm.GetString("Client.Core_1629"));
    Services.eventCategories.Add((object) 24, (object) LocalizationHolder.rm.GetString("Client.Core_1630"));
  }

  private static void UpdateAuditTypes()
  {
    Services.auditTypes.Flush();
    EventlogRecordType[] values = (EventlogRecordType[]) Enum.GetValues(typeof (EventlogRecordType));
    for (int index = 0; index < values.Length; ++index)
      Services.auditTypes.Add((object) (int) values[index], (object) EventlogRecordTypeHelper.GetCaption(values[index]));
  }

  private static void UpdateEventTypes()
  {
    Services.eventTypes.Flush();
    ActionType[] values = (ActionType[]) Enum.GetValues(typeof (ActionType));
    for (int index = 0; index < values.Length; ++index)
      Services.eventTypes.Add((object) (int) values[index], (object) ActionTypeHelper.GetCaption(values[index]));
  }
}
