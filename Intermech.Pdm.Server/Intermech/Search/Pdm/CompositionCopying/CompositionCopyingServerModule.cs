// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionCopying.CompositionCopyingServerModule
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces.Server;
using Intermech.Search.GroupAttributesChanging;
using Intermech.Search.Pdm.Instances;
using System;

#nullable disable
namespace Intermech.Search.Pdm.CompositionCopying;

public sealed class CompositionCopyingServerModule
{
  private ICustomServices _customServices;
  private IInstancesServerService _instancesServerService;
  private IGroupAttributesChangingServerService _groupAttributesChangingServerService;

  public CompositionCopyingServerModule(
    ICustomServices customServices,
    IInstancesServerService instancesServerService,
    IGroupAttributesChangingServerService groupAttributesChangingServerService)
  {
    if (customServices == null)
      throw new ArgumentNullException(nameof (customServices));
    if (instancesServerService == null)
      throw new ArgumentNullException(nameof (instancesServerService));
    if (groupAttributesChangingServerService == null)
      throw new ArgumentNullException(nameof (groupAttributesChangingServerService));
    this._customServices = customServices;
    this._instancesServerService = instancesServerService;
    this._groupAttributesChangingServerService = groupAttributesChangingServerService;
  }

  public void Load()
  {
    this._customServices.AddService(typeof (ICompositionCopyingServerService), (object) new CompositionCopyingServerService(this._instancesServerService, this._groupAttributesChangingServerService));
  }

  public void Unload()
  {
    this._customServices.RemoveService(typeof (ICompositionCopyingServerService));
  }
}
