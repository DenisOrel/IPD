
// Type: Intermech.Navigator.Parts.CompositeUpdateAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

internal class CompositeUpdateAnalyser : IUpdateAnalyser, INavigatorTreeViewUpdateAnalyzer
{
  private List<AnalyserSlot> nativeAnalysers;
  private CompositeUpdatePlan planWrapper;

  public CompositeUpdateAnalyser(List<AnalyserSlot> nativeAnalysers)
  {
    this.nativeAnalysers = nativeAnalysers;
    this.planWrapper = new CompositeUpdatePlan();
  }

  void IUpdateAnalyser.Preprocess(IUpdatePlan plan)
  {
    this.planWrapper.NativePlan = plan;
    for (int index = 0; index < this.nativeAnalysers.Count; ++index)
    {
      this.planWrapper.PartId = this.nativeAnalysers[index].UniqueId;
      this.nativeAnalysers[index].Object.Preprocess((IUpdatePlan) this.planWrapper);
    }
  }

  void IUpdateAnalyser.Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (nodeID == null || nodeID.Cookie == null)
      return;
    int partId = ((PartCookie) nodeID.Cookie).PartId;
    for (int index = 0; index < this.nativeAnalysers.Count; ++index)
    {
      if (this.nativeAnalysers[index].UniqueId == partId)
      {
        this.planWrapper.NativePlan = plan;
        this.planWrapper.PartId = partId;
        this.nativeAnalysers[index].Object.Process(nodeID, (IUpdatePlan) this.planWrapper);
        break;
      }
    }
  }

  void IUpdateAnalyser.Postprocess(IUpdatePlan plan)
  {
    this.planWrapper.NativePlan = plan;
    for (int index = 0; index < this.nativeAnalysers.Count; ++index)
    {
      this.planWrapper.PartId = this.nativeAnalysers[index].UniqueId;
      this.nativeAnalysers[index].Object.Postprocess((IUpdatePlan) this.planWrapper);
    }
  }

  public void Process(NavigatorTreeNode node, IUpdatePlan updatePlan)
  {
    if (node.NodeID == null)
      return;
    int partId = ((PartCookie) node.NodeID.Cookie).PartId;
    foreach (AnalyserSlot nativeAnalyser in this.nativeAnalysers)
    {
      if (nativeAnalyser.UniqueId == partId && nativeAnalyser.Object is INavigatorTreeViewUpdateAnalyzer)
      {
        this.planWrapper.NativePlan = updatePlan;
        this.planWrapper.PartId = partId;
        ((INavigatorTreeViewUpdateAnalyzer) nativeAnalyser.Object).Process(node, (IUpdatePlan) this.planWrapper);
        break;
      }
    }
  }
}
