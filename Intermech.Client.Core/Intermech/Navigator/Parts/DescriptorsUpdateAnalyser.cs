
// Type: Intermech.Navigator.Parts.DescriptorsUpdateAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Parts;

internal class DescriptorsUpdateAnalyser : IUpdateAnalyser
{
  private List<AnalyserSlot> nativeAnalysers;
  private DescriptorsUpdatePlan planWrapper;

  public DescriptorsUpdateAnalyser(List<AnalyserSlot> nativeAnalysers)
  {
    this.nativeAnalysers = nativeAnalysers;
    this.planWrapper = new DescriptorsUpdatePlan();
  }

  void IUpdateAnalyser.Preprocess(IUpdatePlan plan)
  {
    this.planWrapper.NativePlan = plan;
    for (int index = 0; index < this.nativeAnalysers.Count; ++index)
    {
      this.planWrapper.DescriptorId = this.nativeAnalysers[index].UniqueId;
      this.nativeAnalysers[index].Object.Preprocess((IUpdatePlan) this.planWrapper);
    }
  }

  void IUpdateAnalyser.Process(INodeID nodeID, IUpdatePlan plan)
  {
    int descriptorId = ((DescriptorCookie) nodeID.Cookie).DescriptorId;
    for (int index = 0; index < this.nativeAnalysers.Count; ++index)
    {
      if (this.nativeAnalysers[index].UniqueId == descriptorId)
      {
        this.planWrapper.NativePlan = plan;
        this.planWrapper.DescriptorId = descriptorId;
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
      this.planWrapper.DescriptorId = this.nativeAnalysers[index].UniqueId;
      this.nativeAnalysers[index].Object.Postprocess((IUpdatePlan) this.planWrapper);
    }
  }
}
