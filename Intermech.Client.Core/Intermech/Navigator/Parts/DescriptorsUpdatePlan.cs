
// Type: Intermech.Navigator.Parts.DescriptorsUpdatePlan
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Parts;

internal class DescriptorsUpdatePlan : IUpdatePlan
{
  private IUpdatePlan nativePlan;
  private int descriptorId;

  public IUpdatePlan NativePlan
  {
    get => this.nativePlan;
    set => this.nativePlan = value;
  }

  public int DescriptorId
  {
    get => this.descriptorId;
    set => this.descriptorId = value;
  }

  void IUpdatePlan.Append(INodeID partialNodeID)
  {
    partialNodeID.Cookie = (object) new DescriptorCookie(this.descriptorId);
    this.nativePlan.Append(partialNodeID);
  }

  void IUpdatePlan.Update() => this.nativePlan.Update();

  void IUpdatePlan.Replace(INodeID replacementNodeID)
  {
    replacementNodeID.Cookie = (object) new DescriptorCookie(this.descriptorId);
    this.nativePlan.Replace(replacementNodeID);
  }

  void IUpdatePlan.Remove() => this.nativePlan.Remove();
}
