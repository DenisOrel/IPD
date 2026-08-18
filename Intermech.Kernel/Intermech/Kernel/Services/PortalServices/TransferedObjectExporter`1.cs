// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TransferedObjectExporter`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.WebPortal;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class TransferedObjectExporter<TTransferedObject> where TTransferedObject : ITransferedObject
{
  protected readonly TTransferedObject unit;
  protected readonly long portalTaskID;

  public TransferedObjectExporter(long portalTaskID, TTransferedObject unit)
  {
    this.portalTaskID = portalTaskID;
    this.unit = unit;
  }
}
