// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.FileStorages.DataVaultServiceHolder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;


namespace Intermech.Kernel.FileStorages;

public static class DataVaultServiceHolder
{
  public static void RegisterService()
  {
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service))
      return;
    VaultFileReaderService serviceInstance1 = new VaultFileReaderService();
    service.AddService(typeof (IVaultFileReaderService), (object) serviceInstance1);
    DataVaultServiceWork serviceInstance2 = new DataVaultServiceWork();
    service.AddService(typeof (IDataVaultServiceWork), (object) serviceInstance2);
  }
}
