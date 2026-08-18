// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterChangedAnalyser
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

public class FilterChangedAnalyser : IUpdateAnalyser
{
  private INodeItems _owner;
  private Guid _filterGuid;

  public FilterChangedAnalyser(INodeItems owner, Guid filterGuid)
  {
    this._owner = owner;
    this._filterGuid = filterGuid;
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    IFilterGuid data = (IFilterGuid) this._owner.GetData(nodeID, typeof (IFilterGuid));
    if (data == null || !(data.Value == this._filterGuid))
      return;
    plan.Update();
  }

  public void Postprocess(IUpdatePlan plan)
  {
  }
}
