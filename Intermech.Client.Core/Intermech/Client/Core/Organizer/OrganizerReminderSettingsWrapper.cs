
// Type: Intermech.Client.Core.Organizer.OrganizerReminderSettingsWrapper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Client.Core.Organizer;

/// <summary>Враппер для OrganizerReminderSettingsPage.</summary>
internal class OrganizerReminderSettingsWrapper
{
  private IServiceProvider _provider;
  private int _interval = 15;
  private bool _activate = true;
  private bool _inited;
  private bool _modified;
  private int _timeBeforeReminder = 30;

  /// <summary>Включение/отключение уведомлений.</summary>
  [TypeConverter(typeof (YesNoBooleanConverter))]
  [CustomDescription("Attribute_Organizer_Reminder_Settings_Activate_Description")]
  [CustomDisplayName("Attribute_Organizer_Reminder_Settings_Activate_Name")]
  public bool Activate
  {
    get
    {
      this.CheckInited();
      return this._activate;
    }
    set
    {
      this._activate = value;
      this._modified = true;
    }
  }

  /// <summary>Интервал времени между запросами на сервер.</summary>
  [CustomDescription("Attribute_Organizer_Reminder_Settings_Interval_Description")]
  [CustomDisplayName("Attribute_Organizer_Reminder_Settings_Interval_Name")]
  public int Interval
  {
    get
    {
      this.CheckInited();
      return this._interval;
    }
    set
    {
      if (value < 1)
        return;
      this._interval = value;
      this._modified = true;
    }
  }

  /// <summary>
  /// Время до начала напоминания (при инициализации напоминания), 30 мин по умолчанию.
  /// </summary>
  [CustomDescription("Attribute_Organizer_Reminder_Settings_Interval_Before_Description")]
  [CustomDisplayName("Attribute_Organizer_Reminder_Settings_Interval_Before")]
  [DefaultValue(30)]
  public int TimeBeforeReminderInitialize
  {
    get
    {
      this.CheckInited();
      return this._timeBeforeReminder;
    }
    set
    {
      this._timeBeforeReminder = value;
      this._modified = true;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="provider">Контейнер сервисов</param>
  internal OrganizerReminderSettingsWrapper(IServiceProvider provider) => this._provider = provider;

  /// <summary>Считывание настроек.</summary>
  internal bool Load()
  {
    if (!(ServicesManager.GetService(typeof (IDBConfigurations)) is IDBConfigurations service))
      return false;
    this._activate = service.ReadBool("CLIENT", "ORGANIZER_REMINDER", "ACTIVATE", true, DBConfigMode.UserAndGlobal);
    this._interval = Convert.ToInt32(service.ReadInteger("CLIENT", "ORGANIZER_REMINDER", "TIME_SPACE", 15L, DBConfigMode.UserAndGlobal));
    this._timeBeforeReminder = Convert.ToInt32(service.ReadInteger("CLIENT", "ORGANIZER_REMINDER", "TIME_BEFORE", 30L, DBConfigMode.UserAndGlobal));
    this._modified = false;
    return true;
  }

  /// <summary>Обнуление изменений.</summary>
  internal void ResetValues() => this._inited = false;

  /// <summary>Сохранение настроек.</summary>
  /// <param name="session">Сессия пользователя</param>
  internal void Save(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    if (!this._modified)
      return;
    configurations.WriteBool("CLIENT", "ORGANIZER_REMINDER", "ACTIVATE", this._activate, session.UserID);
    configurations.WriteInteger("CLIENT", "ORGANIZER_REMINDER", "TIME_SPACE", (long) this._interval, session.UserID);
    configurations.WriteInteger("CLIENT", "ORGANIZER_REMINDER", "TIME_BEFORE", (long) this._timeBeforeReminder, session.UserID);
    this._modified = false;
    if (this._provider == null || !(this._provider.GetService(typeof (IOrganizerService)) is OrganizerService service))
      return;
    if (this._activate)
      service.StartTimers(this._interval);
    else
      service.StopTimers();
    service.TimeBeforeReminder = this._timeBeforeReminder;
  }

  /// <summary>
  /// 
  /// </summary>
  private void CheckInited()
  {
    if (this._inited)
      return;
    this._inited = this.Load();
  }
}
