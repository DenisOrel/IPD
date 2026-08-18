// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.Compositions.Loading.ShortMeasuredValue
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.Compositions.Loading;

internal class ShortMeasuredValue
{
  public double Value;
  public long MeasureID;
  public readonly long PhysicalQuantityID;
  public readonly bool RootValue;

  public ShortMeasuredValue(
    long measureId,
    long physicalQuantityId,
    double value,
    Dictionary<long, long> measureDescriptors)
  {
    this.MeasureID = measureId;
    if (physicalQuantityId == 0L && measureId != 0L)
    {
      if (!measureDescriptors.TryGetValue(measureId, out this.PhysicalQuantityID))
        this.PhysicalQuantityID = ShortMeasuredValue.GetPhysicalQuantityId(measureId);
    }
    else
      this.PhysicalQuantityID = physicalQuantityId;
    this.Value = value;
    this.RootValue = false;
  }

  public ShortMeasuredValue(
    long measureId,
    double value,
    Dictionary<long, long> measureDescriptors)
    : this(measureId, 0L, value, measureDescriptors)
  {
  }

  public ShortMeasuredValue(MeasuredValue value, Dictionary<long, long> measureDescriptors)
    : this(value.MeasureID, value.Value, measureDescriptors)
  {
  }

  public ShortMeasuredValue()
    : this(0L, 0.0, (Dictionary<long, long>) null)
  {
    this.RootValue = true;
  }

  public MeasuredValue ToMeasuredValue()
  {
    return new MeasuredValue(this.Value, this.MeasureID, string.Empty);
  }

  public static long GetPhysicalQuantityId(long measureId)
  {
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measureId);
    return !descriptor.Empty ? descriptor.PhysicalQuantityID : 0L;
  }
}
