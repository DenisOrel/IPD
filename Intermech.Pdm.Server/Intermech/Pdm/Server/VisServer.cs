// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.VisServer
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Pdm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;

#nullable disable
namespace Intermech.Pdm.Server;

public class VisServer : LongLifeObject, IVisualizerService
{
  private ConcurrentDictionary<long, VisTask> _taskDict;
  private long _taskCounter;
  private ICompositionLoadService _compositionLoadService;
  private IVersionRulesCacheService _versionRulesService;
  private ISelectionsService _selectionsService;
  internal static readonly VisServer vs = new VisServer();
  internal static Dictionary<long, VisServer.RuleIdInfo> RulesList;

  public static VisServer Init(IServiceProvider serviceProvider)
  {
    VisServer.vs._taskDict = new ConcurrentDictionary<long, VisTask>();
    VisServer.vs._compositionLoadService = serviceProvider.GetService<ICompositionLoadService>();
    VisServer.vs._versionRulesService = serviceProvider.GetService<IVersionRulesCacheService>();
    VisServer.vs._selectionsService = serviceProvider.GetService<ISelectionsService>();
    VisServer.RulesList = new Dictionary<long, VisServer.RuleIdInfo>();
    VisServer.AllRelationList = MetaDataHelper.GetRelationTypesList().ConvertAll<int>((Converter<IMSRelationType, int>) (imsRt => imsRt.RelationTypeID));
    return VisServer.vs;
  }

  public static ICompositionLoadService GetCompLoadService()
  {
    return VisServer.vs._compositionLoadService;
  }

  public static ISelectionsService GetSelectionsService() => VisServer.vs._selectionsService;

  public static List<int> AllRelationList { get; private set; }

  public long StartBuildChildTree(
    long projVId,
    long schemeId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    Guid userSession,
    HiddenCompositionFiltrationMode hcfm,
    RelFilter relFilter,
    HybridDictionary dict,
    int levelsOverride = -1,
    int previewMode = 1)
  {
    long num = Interlocked.Increment(ref this._taskCounter);
    VisTask visTask = new VisTask((object) userSession, true, num, schemeId);
    visTask.SetParms(projVId, filtrationOwnerId, rule, relFilter, dict, hcfm: hcfm, levelsOver: levelsOverride, previewMode: previewMode);
    this._taskDict.TryAdd(num, visTask);
    visTask.StartLoadData();
    return num;
  }

  public long StartBuildParentTree(
    long projVId,
    long projId,
    long schemeId,
    string filtrationOwnerId,
    ICompositionsAutosortRule rule,
    HiddenCompositionFiltrationMode hcfm,
    Guid userSession,
    RelFilter relFilter,
    HybridDictionary dict,
    int levelsOverride = -1,
    int previewMode = 1)
  {
    long num = Interlocked.Increment(ref this._taskCounter);
    VisTask visTask = new VisTask((object) userSession, false, num, schemeId);
    visTask.SetParms(projVId, filtrationOwnerId, rule, relFilter, dict, projId, hcfm, levelsOverride, previewMode);
    this._taskDict.TryAdd(num, visTask);
    visTask.StartLoadData();
    return num;
  }

  public long StartCollectPreviews(long[] objIds, Guid userSession, long schemeId = 0)
  {
    long num = Interlocked.Increment(ref this._taskCounter);
    VisTask visTask = new VisTask((object) userSession, false, num, schemeId, objIds);
    visTask.PreviewMode = schemeId != 0L ? 1 : 2;
    this._taskDict.TryAdd(num, visTask);
    visTask.StartLoadData();
    return num;
  }

  public RelVisState GetTaskStatus(long taskId)
  {
    VisTask visTask;
    return !this._taskDict.TryGetValue(taskId, out visTask) ? RelVisState.Unknown : visTask.State;
  }

  public void KillTask(long taskId)
  {
    VisTask visTask;
    if (!this._taskDict.TryGetValue(taskId, out visTask))
      return;
    visTask.KillTask();
    this._taskDict.TryRemove(taskId, out visTask);
  }

  public HybridTableExp GetTaskResult(long taskId)
  {
    VisTask visTask;
    if (!this._taskDict.TryGetValue(taskId, out visTask))
      return (HybridTableExp) null;
    lock (visTask)
      return visTask.ResTable;
  }

  public Exception GetError(long taskId)
  {
    VisTask visTask;
    return !this._taskDict.TryGetValue(taskId, out visTask) ? (Exception) null : visTask.Error;
  }

  public static string GetFiltrationOwnerId(OpParmVersionRule opvr, IUserSession ius)
  {
    if (VisServer.vs._versionRulesService == null)
      return "";
    string filtrationOwnerId = VisServer.RulesList.ContainsKey(opvr.ruleId) ? VisServer.RulesList[opvr.ruleId].OwnerId : "";
    FiltrationSettings filtrationSettings;
    if (filtrationOwnerId != "")
    {
      filtrationSettings = VisServer.vs._versionRulesService.GetFiltrationSettings((object) ius, filtrationOwnerId);
    }
    else
    {
      filtrationOwnerId = opvr.ruleGuid;
      filtrationSettings = VisServer.CreateFiltSettings(filtrationOwnerId, opvr.ruleId);
      filtrationSettings.CurrentRule = VisServer.vs._versionRulesService[opvr.ruleId];
    }
    VisServer.vs._versionRulesService.SetFiltrationSettings((object) ius, filtrationOwnerId, filtrationSettings);
    return filtrationOwnerId;
  }

  internal static FiltrationSettings CreateFiltSettings(string ownerId, long ruleId)
  {
    FiltrationSettings filtSettings = new FiltrationSettings();
    filtSettings.OwnerID = ownerId;
    VisServer.RulesList.Add(ruleId, new VisServer.RuleIdInfo(ruleId, ownerId));
    return filtSettings;
  }

  internal class RuleIdInfo
  {
    public long ObjRuleId;
    public string OwnerId;

    public override bool Equals(object obj)
    {
      return obj is VisServer.RuleIdInfo && ((VisServer.RuleIdInfo) obj).ObjRuleId == this.ObjRuleId && ((VisServer.RuleIdInfo) obj).OwnerId == this.OwnerId;
    }

    public override int GetHashCode() => (int) this.ObjRuleId;

    public RuleIdInfo(long ruleId, string oId)
    {
      this.ObjRuleId = ruleId;
      this.OwnerId = oId;
    }
  }
}
