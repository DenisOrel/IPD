// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportThreadArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class ImportThreadArgs
{
  public IUserSession Session { get; private set; }

  public string UpdateGuid { get; private set; }

  public long TaskID { get; set; }

  public long[] ObjectsIDs { get; private set; }

  public bool StartImmediately { get; private set; }

  public ImportThreadArgs(
    IUserSession session,
    string updateGuid,
    long taskID,
    long[] objectsIDs,
    bool startImmediately)
  {
    this.Session = session;
    this.UpdateGuid = updateGuid;
    this.TaskID = taskID;
    this.ObjectsIDs = objectsIDs;
    this.StartImmediately = startImmediately;
  }
}
