// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.CustomXMLFileFormer`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;


namespace Intermech.Kernel.Services.PortalServices;

public class CustomXMLFileFormer<T> : XMLFileFormer
{
  protected T data;

  public CustomXMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    T data)
    : base(session, unit, writer)
  {
    this.data = data;
  }
}
