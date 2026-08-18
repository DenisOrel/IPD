// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class UserSessionCollection : KernelRoot, IUserSessionCollection
{
  private ConcurrentDictionary<Guid, UserSession> _SessionsList;
  private int _SessionID;

  public UserSessionCollection()
  {
    this._SessionsList = new ConcurrentDictionary<Guid, UserSession>();
    this._SessionID = 0;
  }

  public IUserSession GetSession(Guid sessionGUID)
  {
    UserSession userSession;
    return this._SessionsList.TryGetValue(sessionGUID, out userSession) ? (IUserSession) userSession : (IUserSession) null;
  }

  internal UserSession GetSessionByUserID(long userID, int maxIdleTime, string computerName)
  {
    foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
    {
      ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
      if (threadedAccessWrapper.UserID == userID && threadedAccessWrapper.SessionStatus == UserSessionStatus.Logged && threadedAccessWrapper.ComputerName != computerName && (maxIdleTime <= 0 || maxIdleTime > 0 && DateTime.UtcNow < threadedAccessWrapper.LastCallTime + TimeSpan.FromMinutes((double) maxIdleTime)))
        return threadedAccessWrapper.Session;
    }
    return (UserSession) null;
  }

  public bool IsLoggedIn(long userID)
  {
    foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
    {
      ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
      if (threadedAccessWrapper.UserID == userID && threadedAccessWrapper.SessionStatus == UserSessionStatus.Logged && DateTime.UtcNow < threadedAccessWrapper.LastCallTime + ServerConsts.OldSessionsInactivityInterval)
        return true;
    }
    return false;
  }

  public string[] PrintSessions(string fileName, bool toConsole)
  {
    List<string> stringList = new List<string>();
    Dictionary<Guid, UserSessionCollection.SessionInfo> dictionary = new Dictionary<Guid, UserSessionCollection.SessionInfo>(this._SessionsList.Count);
    List<Guid> guidList = new List<Guid>();
    int num1 = 0;
    foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
    {
      ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
      UserSessionCollection.SessionInfo sessionInfo;
      if (threadedAccessWrapper.ParentSession == null)
      {
        if (threadedAccessWrapper.UserID != 0L)
        {
          if (dictionary.TryGetValue(threadedAccessWrapper.SessionGUID, out sessionInfo))
            sessionInfo.AddActivity(threadedAccessWrapper.CallCounter, 0);
          else
            dictionary.Add(sessions.Key, new UserSessionCollection.SessionInfo(threadedAccessWrapper.CallCounter));
        }
      }
      else if (threadedAccessWrapper.UserID != 0L)
      {
        if (dictionary.TryGetValue(threadedAccessWrapper.ParentSession.SessionGUID, out sessionInfo))
        {
          sessionInfo.AddActivity(threadedAccessWrapper.CallCounter, 1);
        }
        else
        {
          sessionInfo = new UserSessionCollection.SessionInfo(0);
          sessionInfo.AddActivity(threadedAccessWrapper.CallCounter, 1);
          dictionary.Add(threadedAccessWrapper.ParentSession.SessionGUID, sessionInfo);
        }
      }
      if (threadedAccessWrapper.UserID != 0L && threadedAccessWrapper.SessionStatus != UserSessionStatus.Closing && threadedAccessWrapper.InTransaction)
      {
        ++num1;
        Guid guid = threadedAccessWrapper.ParentSession != null ? threadedAccessWrapper.ParentSession.SessionGUID : threadedAccessWrapper.SessionGUID;
        if (guidList.IndexOf(guid) < 0)
          guidList.Add(guid);
      }
    }
    int num2 = -1;
    foreach (KeyValuePair<Guid, UserSessionCollection.SessionInfo> keyValuePair in dictionary)
    {
      UserSession sessions = this._SessionsList[keyValuePair.Key];
      if (sessions != null)
      {
        string str1 = string.Empty;
        string str2;
        if (sessions.IsNotLogged)
        {
          str2 = "Сессия закрыта";
        }
        else
        {
          str2 = keyValuePair.Value.ActiviryCount.ToString();
          if (guidList.IndexOf(sessions.SessionGUID) >= 0)
            str1 = ". В сессии или в ее клонах есть открытая транзакция.";
        }
        string str3 = $"Пользователь: {sessions.UserName}, компьютер: {sessions.ComputerName}, клоны: {keyValuePair.Value.ClonesCount}, последний вызов: {sessions.LastCallTime + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now)}, счетчик активности: {str2}{str1}";
        if (toConsole)
          Console.WriteLine(str3);
        else
          stringList.Add(str3);
        ++num2;
      }
    }
    string str4 = "Всего пользовательских сессий: " + num2.ToString();
    if (toConsole)
      Console.WriteLine(str4);
    else
      stringList.Add(str4);
    string str5 = "Всего открытых транзакций в сессиях: " + num1.ToString();
    if (toConsole)
      Console.WriteLine(str5);
    else
      stringList.Add(str5);
    if (toConsole)
    {
      for (int index = 0; index < stringList.Count; ++index)
        Console.WriteLine(stringList[index]);
      stringList.Clear();
    }
    return stringList.ToArray();
  }

  public int ActivityCounter
  {
    get
    {
      int activityCounter = 0;
      foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
      {
        ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
        activityCounter = activityCounter + threadedAccessWrapper.CallCounter + 1;
      }
      return activityCounter;
    }
  }

  internal int CreateSessionID() => Interlocked.Increment(ref this._SessionID);

  internal void AddSession(UserSession uSession)
  {
    this._SessionsList.TryAdd(uSession.SessionGUID, uSession);
  }

  internal void DeleteSession(UserSession uSession)
  {
    this._SessionsList.TryRemove(uSession.SessionGUID, out UserSession _);
  }

  internal IList<KeyValuePair<Guid, UserSession>> GetGuidsAndSessions()
  {
    return (IList<KeyValuePair<Guid, UserSession>>) this._SessionsList.ToArray();
  }

  internal void ClearActiveStorageID(long storageID)
  {
    foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
    {
      ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
      if (storageID == 0L)
        threadedAccessWrapper.ModifyActiveStorageID(threadedAccessWrapper.ActiveStorageID, 0L);
      else if (threadedAccessWrapper.ActiveStorageID == storageID)
        threadedAccessWrapper.ModifyActiveStorageID(storageID, 0L);
    }
  }

  internal void SetDBSecurityClearCacheFlag(long userID)
  {
    foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
    {
      ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
      if (userID == 0L || threadedAccessWrapper.UserID == userID)
        threadedAccessWrapper.TryGetDBSecurity()?.RaceSetClearCacheFlag();
    }
  }

  internal bool ExistsLoggedClones(Guid sessionGUID)
  {
    foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
    {
      ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
      if (threadedAccessWrapper.ParentSession != null && threadedAccessWrapper.ParentSession.SessionGUID == sessionGUID && threadedAccessWrapper.SessionStatus == UserSessionStatus.Logged)
        return true;
    }
    return false;
  }

  internal void SetNotifySamlpesIsModifiedFlag(Guid masterSessionGUID)
  {
    foreach (KeyValuePair<Guid, UserSession> sessions in this._SessionsList)
    {
      ThreadedAccessWrapper threadedAccessWrapper = sessions.Value.GetThreadedAccessWrapper();
      if (threadedAccessWrapper.MasterSessionGUID == masterSessionGUID)
        threadedAccessWrapper.TryGetNSProcessor()?.RaceSetIsModified();
    }
  }

  private class SessionInfo
  {
    public int ClonesCount { get; private set; }

    public int ActiviryCount { get; private set; }

    public SessionInfo(int activityCounter)
    {
      this.ClonesCount = 0;
      this.ActiviryCount = activityCounter;
    }

    public void AddActivity(int activityCounter, int clonesCounter)
    {
      this.ActiviryCount += activityCounter;
      this.ClonesCount += clonesCounter;
    }
  }
}
