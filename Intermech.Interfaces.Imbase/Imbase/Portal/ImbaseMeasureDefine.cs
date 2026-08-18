// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.ImbaseMeasureDefine
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.Portal;

internal class ImbaseMeasureDefine : MeasureDefine
{
  private IUserSession _session;

  public ImbaseMeasureDefine(IUserSession session) => this._session = session;

  private Guid GetMeasureGuid(long measureID) => this._session.GetObject(measureID).ObjectGUID;

  protected override Guid FindMeasureGuid(string unit)
  {
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(unit);
    return descriptor != null && !descriptor.Empty ? this.GetMeasureGuid(descriptor.MeasureID) : base.FindMeasureGuid(unit);
  }

  protected override Guid FindDefaultMeasureGuid(long physicalValueID)
  {
    long baseMeasureId = MeasureHelper.GetBaseMeasureID(physicalValueID);
    switch (baseMeasureId)
    {
      case -1:
      case 0:
        return base.FindDefaultMeasureGuid(physicalValueID);
      default:
        return this.GetMeasureGuid(baseMeasureId);
    }
  }
}
