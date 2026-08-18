// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.EmailDownloadService
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using Intermech.Interfaces.Workflow.Email;
using Intermech.Kernel;
using Intermech.Workflow.Server.Email;
using System;
using System.Collections.Generic;
using System.Timers;

#nullable disable
namespace Intermech.Workflow.Server;

internal class EmailDownloadService : LongLifeObject, IEmailDownloadService
{
  private ICalendar _calendar;
  private Dictionary<Guid, EmailDownloader> _downloaders = new Dictionary<Guid, EmailDownloader>();
  private IUserSession _session;
  private bool _downloading;
  private EmailDownloadSettings _settings;
  private List<string> _processAccaunts;
  private Timer _timer;
  private bool _timerStarted;
  private object locker = new object();

  public EmailDownloadService()
  {
    this._session = (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionPermanentClone(nameof (EmailDownloadService));
    this._timer = new Timer();
    this._timerStarted = false;
    this._processAccaunts = new List<string>();
    this._settings = new EmailDownloadSettings();
    this.ReloadSettings();
  }

  private void DownloadTimerElapsed(object sender, ElapsedEventArgs e)
  {
    if (this._downloading)
      return;
    try
    {
      this._downloading = true;
      if (this._settings.WorkTimeOnly)
      {
        bool flag = false;
        ICalendarDay dayByDate = this._calendar.GetDayByDate(DateTime.Now);
        if (dayByDate.DayType != DayType.Holiday && dayByDate.WorkTimePeriods != null)
        {
          TimeSpan timeSpan1;
          ref TimeSpan local = ref timeSpan1;
          DateTime now = DateTime.Now;
          int hour = now.Hour;
          now = DateTime.Now;
          int minute = now.Minute;
          local = new TimeSpan(hour, minute, 0);
          foreach (IWorkTimePeriod workTimePeriod in (IEnumerable<IWorkTimePeriod>) dayByDate.WorkTimePeriods)
          {
            TimeSpan timeSpan2 = new TimeSpan(workTimePeriod.StartHours, workTimePeriod.StartMinutes, 0);
            TimeSpan timeSpan3 = new TimeSpan(workTimePeriod.FinishHours, workTimePeriod.FinishMinutes, 0);
            if (timeSpan2.Equals(timeSpan1) || timeSpan3.Equals(timeSpan1) || timeSpan1 > timeSpan2 && timeSpan1 < timeSpan3)
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag)
          return;
      }
      IEmailService customService = (IEmailService) this._session.GetCustomService(typeof (IEmailService));
      if (customService == null)
        return;
      EmailServer[] servers = customService.Servers;
      if (servers == null)
        return;
      for (int index1 = 0; index1 < servers.Length; ++index1)
      {
        EmailAccaunt[] accaunts = customService.GetAccaunts(servers[index1].Guid);
        if (accaunts != null)
        {
          for (int index2 = 0; index2 < accaunts.Length; ++index2)
          {
            lock (this._processAccaunts)
            {
              if (!this._processAccaunts.Contains(accaunts[index2].Email))
                this._processAccaunts.Add(accaunts[index2].Email);
              else
                continue;
            }
            EmailDownloader emailDownloader = new EmailDownloader(accaunts[index2].Email, this._settings.RemoveMessages, true);
            emailDownloader.DownloadCompleteEvent += new DownloadCompleteEventHandler(this.AccauntDownloadComplete);
            emailDownloader.StartDownload(this._session);
          }
        }
      }
    }
    catch (Exception ex)
    {
      (ApplicationServices.Container.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace(string.Format(LocalizationHolder.rm.GetString("Workflow.Server_44"), (object) ex.Message), Consts.traceError, string.Empty);
    }
    finally
    {
      this._downloading = false;
    }
  }

  private void AccauntDownloadComplete(object sender, DownloadCompleteEventArgs e)
  {
    lock (this._processAccaunts)
    {
      if (!this._processAccaunts.Contains(e.AccauntEmail))
        return;
      this._processAccaunts.Remove(e.AccauntEmail);
    }
  }

  public void StartDownload(
    Guid sessionGuid,
    Guid processID,
    string accauntEmal,
    bool removeMessages)
  {
    EmailDownloader emailDownloader = new EmailDownloader(accauntEmal, removeMessages, false);
    this._downloaders.Add(processID, emailDownloader);
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    emailDownloader.StartDownload(sessionById);
  }

  public EmailDownloadProperties GetDownloadProperties(Guid processID)
  {
    EmailDownloader emailDownloader;
    return this._downloaders.TryGetValue(processID, out emailDownloader) ? emailDownloader.Properties : (EmailDownloadProperties) null;
  }

  public void CompleteDownload(Guid processID) => this._downloaders.Remove(processID);

  public void StopDownload(Guid processID)
  {
    EmailDownloader emailDownloader;
    if (!this._downloaders.TryGetValue(processID, out emailDownloader))
      return;
    emailDownloader.StopDownload();
  }

  public void ReloadSettings()
  {
    lock (this.locker)
    {
      if (this._timerStarted)
      {
        this._timer.Elapsed -= new ElapsedEventHandler(this.DownloadTimerElapsed);
        this._timer.Stop();
      }
      lock (this._settings)
      {
        (this._session as UserSession).DBCache.ReloadTables(this._session, (this._session as UserSession).DataManager, "IMS_CONFIGS");
        this._settings.Load(this._session);
        if (!this._settings.EnableDownload || !this._settings.ComputerName.ToUpper().Equals(EnvironmentConsts.MachineName.ToUpper()))
          return;
        if (this._settings.WorkTimeOnly)
        {
          IDBObject dbObject = this._session.GetObject(this._settings.CalendarGuid, true);
          this._calendar = ((ICalendarsService) ApplicationServices.Container.GetService(typeof (ICalendarsService))).GetCalendar(dbObject.ObjectID, this._session);
        }
        else
          this._calendar = (ICalendar) null;
        this._timer.Interval = TimeSpan.FromMinutes((double) this._settings.Period).TotalMilliseconds;
        this._timer.Elapsed += new ElapsedEventHandler(this.DownloadTimerElapsed);
        this._timer.Start();
        this._timerStarted = true;
      }
    }
  }
}
