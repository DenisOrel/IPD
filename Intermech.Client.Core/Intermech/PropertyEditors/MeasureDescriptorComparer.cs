
// Type: Intermech.PropertyEditors.MeasureDescriptorComparer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.PropertyEditors;

/// <summary>
/// 
/// </summary>
public class MeasureDescriptorComparer : IComparer<MeasureDescriptor>
{
  private bool byPhysical;
  private bool byK;

  public MeasureDescriptorComparer(bool byPhysical, bool byK)
  {
    this.byPhysical = byPhysical;
    this.byK = byK;
  }

  public int Compare(MeasureDescriptor x, MeasureDescriptor y)
  {
    if (this.byPhysical)
    {
      if (x.PhysicalQuantityID < y.PhysicalQuantityID)
        return -1;
      if (x.PhysicalQuantityID > y.PhysicalQuantityID)
        return 1;
    }
    if (this.byK)
    {
      if (x.K < y.K)
        return -1;
      if (x.K > y.K)
        return 1;
    }
    return 0;
  }
}
