// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterCreatedAnalyser
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

internal class FilterCreatedAnalyser : IUpdateAnalyser
{
  private INodeItems _owner;
  private Guid _filterGuid;
  private bool _filterExists;

  public FilterCreatedAnalyser(INodeItems owner, Guid filterGuid)
  {
    this._owner = owner;
    this._filterGuid = filterGuid;
    this._filterExists = false;
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (this._filterExists)
      return;
    IFilterGuid data = (IFilterGuid) this._owner.GetData(nodeID, typeof (IFilterGuid));
    if (data == null || !(data.Value == this._filterGuid))
      return;
    this._filterExists = true;
  }

  public void Postprocess(IUpdatePlan plan)
  {
    if (this._filterExists)
      return;
    plan.Append((INodeID) new FilterNodeID(this._filterGuid));
  }
}
