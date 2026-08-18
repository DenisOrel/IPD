
// Type: Intermech.Navigator.Parts.CompositeUpdatePlan
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Diagnostics;


namespace Intermech.Navigator.Parts;

internal class CompositeUpdatePlan : IUpdatePlan
{
  private IUpdatePlan nativePlan;
  private int partId;

  public IUpdatePlan NativePlan
  {
    [DebuggerStepThrough] get => this.nativePlan;
    set => this.nativePlan = value;
  }

  public int PartId
  {
    [DebuggerStepThrough] get => this.partId;
    set => this.partId = value;
  }

  void IUpdatePlan.Append(INodeID partialNodeID)
  {
    if (partialNodeID.Cookie == null)
      partialNodeID.Cookie = (object) new PartCookie();
    ((PartCookie) partialNodeID.Cookie).PartId = this.partId;
    this.nativePlan.Append(partialNodeID);
  }

  void IUpdatePlan.Update() => this.nativePlan.Update();

  void IUpdatePlan.Replace(INodeID replacementNodeID)
  {
    if (replacementNodeID.Cookie == null)
      replacementNodeID.Cookie = (object) new PartCookie();
    ((PartCookie) replacementNodeID.Cookie).PartId = this.partId;
    this.nativePlan.Replace(replacementNodeID);
  }

  void IUpdatePlan.Remove() => this.nativePlan.Remove();
}
