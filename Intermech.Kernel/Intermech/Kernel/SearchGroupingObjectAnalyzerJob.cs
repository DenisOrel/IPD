// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SearchGroupingObjectAnalyzerJob
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Threading;


namespace Intermech.Kernel;

public sealed class SearchGroupingObjectAnalyzerJob
{
  public Guid _guid;
  public Guid _userSessionGuid;
  public Thread _thread;
  public ISearchGroupingObjectAnalyzer _analyzer;
  public SearchGroupingObjects _searchGroupingObjects;
  private SearchGroupingObjectJobStatus _status = new SearchGroupingObjectJobStatus();

  public SearchGroupingObjectAnalyzerJob(
    Guid userSessionGuid,
    ISearchGroupingObjectAnalyzer analyzer,
    SearchGroupingObjects searchGroupingObjects)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (analyzer == null)
      throw new ArgumentNullException(nameof (analyzer));
    if (searchGroupingObjects == null)
      throw new ArgumentNullException(nameof (searchGroupingObjects));
    this._userSessionGuid = userSessionGuid;
    this._analyzer = analyzer;
    this._searchGroupingObjects = searchGroupingObjects;
    this._guid = Guid.NewGuid();
  }

  public SearchGroupingObjectJobStatus Status
  {
    get
    {
      lock (this._status)
        return this._status.Clone() as SearchGroupingObjectJobStatus;
    }
  }

  public void Start()
  {
    this._thread = new Thread(new ThreadStart(this.ThreadMethod));
    this._thread.IsBackground = true;
    this._thread.Name = "SearchGroupingObjectAnalyzerJob." + this._guid.ToString();
    this._thread.Start();
  }

  public void Stop() => this._thread = (Thread) null;

  public override bool Equals(object obj)
  {
    return !(obj is SearchGroupingObjectAnalyzerJob objectAnalyzerJob) ? base.Equals(obj) : this._guid.Equals(objectAnalyzerJob._guid);
  }

  public override int GetHashCode() => this._guid.GetHashCode();

  public void ThreadMethod()
  {
    try
    {
      lock (this._status)
      {
        if (this._status.Progress != SearchGroupingObjectJobProgress.NotStarted)
          return;
        this._status.Start();
      }
      int objects = 0;
      try
      {
        IUserSession userSession = (UserSession.GetSessionByID(this._userSessionGuid) as UserSession).Clone(nameof (SearchGroupingObjectAnalyzerJob));
        try
        {
          lock (this._status)
          {
            for (int index = 0; index < this._searchGroupingObjects.Count; ++index)
            {
              if (this._thread == null)
              {
                this._status.Progress = SearchGroupingObjectJobProgress.Cancelled;
                break;
              }
              SearchGroupingObject searchGroupingObject = this._searchGroupingObjects[index];
            }
          }
          objects += this._analyzer.Analyze(userSession, this._searchGroupingObjects);
          lock (this._status)
          {
            this._status.Objects = (long) objects;
            if (this._thread == null)
              this._status.Progress = SearchGroupingObjectJobProgress.Cancelled;
            if (this._status.Progress == SearchGroupingObjectJobProgress.Cancelled)
              this._status.Cancel();
          }
          lock (this._status)
          {
            if (this._status.Progress == SearchGroupingObjectJobProgress.Cancelled)
              return;
            for (int index = 0; index < this._searchGroupingObjects.Count; ++index)
            {
              if (this._thread == null)
              {
                this._status.Progress = SearchGroupingObjectJobProgress.Cancelled;
                break;
              }
              this._searchGroupingObjects[index].LoadDescription(userSession);
            }
          }
        }
        finally
        {
          userSession.Logout(nameof (SearchGroupingObjectAnalyzerJob));
          lock (this._status)
          {
            if (this._status.Progress != SearchGroupingObjectJobProgress.Cancelled)
              this._status.Complete(objects, this._searchGroupingObjects);
          }
        }
      }
      catch (Exception ex)
      {
        lock (this._status)
          this._status.Error(ex, this._searchGroupingObjects);
      }
    }
    catch (Exception ex)
    {
    }
  }
}
