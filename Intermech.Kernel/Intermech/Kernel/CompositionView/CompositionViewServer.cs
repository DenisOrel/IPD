// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.CompositionView.CompositionViewServer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.CompositionView;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel.CompositionView;

public class CompositionViewServer : LongLifeObject, ICompositionViewServer
{
  public static void RegisterService()
  {
    if (!(ServerServices.GetService(typeof (ICustomServices)) is ICustomServices service) || service.GetService(typeof (ICompositionViewServer)) != null)
      return;
    service.AddService(typeof (ICompositionViewServer), (object) new CompositionViewServer());
  }

  public void SaveButtonsSettings(byte[] data)
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IServerSession serverSession = (IServerSession) null;
    try
    {
      serverSession = service.GetSystemSessionTemporaryClone("CVS.SaveButtonsSettings") as IServerSession;
      BlobInformation config_info = new BlobInformation((long) data.Length, (long) data.Length, DateTime.Now, "CompositionViewButtons", ArcMethods.NotPacked, string.Empty);
      serverSession.Configurations.WriteConfigData(config_info, data);
    }
    finally
    {
      serverSession?.Logout("CVS.SaveButtonsSettings");
    }
  }

  public byte[] LoadButtonsSettings()
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IServerSession serverSession = (IServerSession) null;
    try
    {
      serverSession = service.GetSystemSessionTemporaryClone("CVS.LoadButtonsSettings") as IServerSession;
      try
      {
        BlobInformation config_info;
        byte[] config_file;
        serverSession.Configurations.LoadConfigData("CompositionViewButtons", out config_info, out config_file);
        if (config_info.RealFileSize > 0L)
        {
          if (config_file.Length != 0)
            return config_file;
        }
      }
      catch
      {
        return (byte[]) null;
      }
    }
    finally
    {
      serverSession?.Logout("CVS.LoadButtonsSettings");
    }
    return (byte[]) null;
  }
}
