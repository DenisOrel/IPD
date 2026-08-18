// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CadmechServicesModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators.CADInterface;

#nullable disable
namespace Intermech.Tools.Client;

internal sealed class CadmechServicesModule : InitializerModule
{
  protected override void DoInitialize()
  {
    base.DoInitialize();
    ServicesManager.AddService(typeof (ICadmech3DServices), (object) new Cadmech3DServices());
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    ServicesManager.RemoveService(typeof (ICadmech3DServices));
  }
}
