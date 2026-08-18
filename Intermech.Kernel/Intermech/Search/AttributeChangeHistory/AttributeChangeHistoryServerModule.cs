// Decompiled with JetBrains decompiler
// Type: Intermech.Search.AttributeChangeHistory.AttributeChangeHistoryServerModule
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;


namespace Intermech.Search.AttributeChangeHistory;

public sealed class AttributeChangeHistoryServerModule
{
  public void Load()
  {
    ServiceLocator.Get<ICustomServices>().AddService(typeof (IAttributeChangeHistoryServerService), (object) new AttributeChangeHistoryServerService());
  }

  public void Unload()
  {
    ServiceLocator.Get<ICustomServices>().RemoveService(typeof (IAttributeChangeHistoryServerService));
  }
}
