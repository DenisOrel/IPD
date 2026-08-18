// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.AutoSelection.AutoSelectionLog;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionService;

public class AutoSelectionService : IAutoSelectionService
{
  internal static readonly Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLog SelectionLog = new Intermech.AutoSelection.Client.AutoSelectionLog.AutoSelectionLog();
  private BeforeCommitCreation _beforeCommitCreationStorage;
  private AfterCommitCreation _afterCommitCreationStorage;
  private BeforeCreateRelation _beforeCreateRelationStorage;
  private AfterCreateRelation _afterCreateRelationStorage;

  private List<RelObjInfoItem> ExecuteSelectionData(AutoSelectionParams args, bool testMode)
  {
    if (args == null)
      throw new ArgumentNullException(nameof (args));
    Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService.SelectionLog.Clear();
    IExpertUser expertUserService = AutoSelectionUtils.ServiceKeeper.GetExpertUserService();
    if (expertUserService != null)
    {
      int num = expertUserService.ShowTraceWindow ? 1 : 0;
    }
    List<RelObjInfoItem> relObjInfoItemList;
    using (AutoSelectionSession selectionSession = new AutoSelectionSession(this, args))
    {
      relObjInfoItemList = selectionSession.Execute(testMode);
      Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService.SelectionLog.AddRange((IEnumerable<IAutoSelectionLogRec>) selectionSession.SelectionLog);
    }
    return relObjInfoItemList;
  }

  internal void DoBeforeCommitCreation(object sender, ObjectEventArgs e)
  {
    BeforeCommitCreation commitCreationStorage = this._beforeCommitCreationStorage;
    if (commitCreationStorage == null)
      return;
    commitCreationStorage(sender, e);
  }

  internal void DoAfterCommitCreation(object sender, ObjectEventArgs e)
  {
    AfterCommitCreation commitCreationStorage = this._afterCommitCreationStorage;
    if (commitCreationStorage == null)
      return;
    commitCreationStorage(sender, e);
  }

  internal void DoBeforeCreateRelation(object sender, RelationEventArgs e)
  {
    BeforeCreateRelation createRelationStorage = this._beforeCreateRelationStorage;
    if (createRelationStorage == null)
      return;
    createRelationStorage(sender, e);
  }

  internal void DoAfterCreateRelation(object sender, RelationEventArgs e)
  {
    AfterCreateRelation createRelationStorage = this._afterCreateRelationStorage;
    if (createRelationStorage == null)
      return;
    createRelationStorage(sender, e);
  }

  public List<long> ExecuteSelection(long objectId, AutoSelectionMode mode)
  {
    return this.ExecuteSelection(objectId, 0L, mode);
  }

  public List<long> ExecuteSelection(long objectId, long relationId, AutoSelectionMode mode)
  {
    return this.ExecuteSelection(objectId, relationId, false, mode);
  }

  public List<long> ExecuteSelection(
    long objectId,
    long relationId,
    bool testMode,
    AutoSelectionMode mode)
  {
    List<long> relIdList = new List<long>();
    List<RelObjInfoItem> relObjInfoItemList = this.ExecuteSelection(new AutoSelectionParams(objectId, relationId, mode), testMode);
    if (relObjInfoItemList == null || relObjInfoItemList.Count == 0)
      return relIdList;
    relIdList.Capacity = relObjInfoItemList.Count;
    relObjInfoItemList.ForEach((Action<RelObjInfoItem>) (item => relIdList.Add(item.RelationID)));
    return relIdList;
  }

  public List<RelObjInfoItem> ExecuteSelection(AutoSelectionParams args)
  {
    return args != null ? this.ExecuteSelection(args, false) : throw new ArgumentNullException(nameof (args));
  }

  public List<RelObjInfoItem> ExecuteSelection(AutoSelectionParams args, bool testMode)
  {
    return args != null ? this.ExecuteSelectionData(args, testMode) : throw new ArgumentNullException(nameof (args));
  }

  public IAutoSelectionLog GetLastExecuteLog
  {
    get => (IAutoSelectionLog) Intermech.AutoSelection.Client.AutoSelectionService.AutoSelectionService.SelectionLog;
  }

  event BeforeCommitCreation IAutoSelectionService.OnBeforeCommitCreation
  {
    add => this._beforeCommitCreationStorage += value;
    remove => this._beforeCommitCreationStorage -= value;
  }

  event AfterCommitCreation IAutoSelectionService.OnAfterCommitCreation
  {
    add => this._afterCommitCreationStorage += value;
    remove => this._afterCommitCreationStorage -= value;
  }

  event BeforeCreateRelation IAutoSelectionService.OnBeforeCreateRelation
  {
    add => this._beforeCreateRelationStorage += value;
    remove => this._beforeCreateRelationStorage -= value;
  }

  event AfterCreateRelation IAutoSelectionService.OnAfterCreateRelation
  {
    add => this._afterCreateRelationStorage += value;
    remove => this._afterCreateRelationStorage -= value;
  }
}
