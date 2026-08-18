// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SearchGroupingObjectAnalyzerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Kernel;

public sealed class SearchGroupingObjectAnalyzerService : 
  LongLifeObject,
  ISearchGroupingObjectsService
{
  private Dictionary<string, ISearchGroupingObjectAnalyzer> _analyzerDictionaryByName = new Dictionary<string, ISearchGroupingObjectAnalyzer>();
  private Dictionary<Guid, SearchGroupingObjectAnalyzerJob> _jobDictionaryByGuid = new Dictionary<Guid, SearchGroupingObjectAnalyzerJob>();

  public string[] AnalyzerNames => this._analyzerDictionaryByName.Keys.ToArray<string>();

  public Guid Analyze(
    Guid userSessionGuid,
    string analyzerName,
    SearchGroupingObjects searchGroupingObjects)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(analyzerName))
      throw new ArgumentException();
    if (searchGroupingObjects == null)
      throw new ArgumentNullException(nameof (searchGroupingObjects));
    if (!this._analyzerDictionaryByName.ContainsKey(analyzerName))
      throw new Exception();
    SearchGroupingObjectAnalyzerJob objectAnalyzerJob = new SearchGroupingObjectAnalyzerJob(userSessionGuid, this._analyzerDictionaryByName[analyzerName], searchGroupingObjects);
    objectAnalyzerJob.Start();
    lock (this._jobDictionaryByGuid)
      this._jobDictionaryByGuid.Add(objectAnalyzerJob._guid, objectAnalyzerJob);
    return objectAnalyzerJob._guid;
  }

  public SearchGroupingObjectJobStatus QueryJobStatus(Guid jobID)
  {
    lock (this._jobDictionaryByGuid)
    {
      if (!this._jobDictionaryByGuid.ContainsKey(jobID))
        return (SearchGroupingObjectJobStatus) null;
      SearchGroupingObjectAnalyzerJob objectAnalyzerJob = this._jobDictionaryByGuid[jobID];
      SearchGroupingObjectJobStatus status = objectAnalyzerJob.Status;
      if (status.Progress == SearchGroupingObjectJobProgress.NotStarted || status.Progress == SearchGroupingObjectJobProgress.Working)
        return status;
      this._jobDictionaryByGuid.Remove(jobID);
      objectAnalyzerJob.Stop();
      return status;
    }
  }

  public bool CancelJob(Guid jobID)
  {
    lock (this._jobDictionaryByGuid)
    {
      if (!this._jobDictionaryByGuid.ContainsKey(jobID))
        return false;
      SearchGroupingObjectAnalyzerJob objectAnalyzerJob = this._jobDictionaryByGuid[jobID];
      this._jobDictionaryByGuid.Remove(jobID);
      objectAnalyzerJob.Stop();
      return true;
    }
  }

  public void RegisterAnalyzer(ISearchGroupingObjectAnalyzer analyzer)
  {
    if (analyzer == null)
      throw new ArgumentNullException(nameof (analyzer));
    this._analyzerDictionaryByName.Add(analyzer.Name, analyzer);
  }

  public void UnregisterAnalyzer(ISearchGroupingObjectAnalyzer analyzer)
  {
    if (analyzer == null)
      throw new ArgumentNullException(nameof (analyzer));
    this._analyzerDictionaryByName.Remove(analyzer.Name);
  }
}
