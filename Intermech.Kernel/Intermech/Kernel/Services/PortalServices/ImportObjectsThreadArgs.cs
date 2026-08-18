// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportObjectsThreadArgs
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImportObjectsThreadArgs : ImportThreadArgs
{
  public bool SetOwner;
  public bool AutoUpdate;
  public SelectCompositionType CompositionType;
  public int[] FilteredTypes;

  public ImportObjectsThreadArgs(
    IUserSession session,
    string updateGuid,
    long taskID,
    long[] objectsIDs,
    int[] filteredTypes,
    bool setOwner,
    bool autoUpdate,
    SelectCompositionType compositionType,
    bool startImmediately)
    : base(session, updateGuid, taskID, objectsIDs, startImmediately)
  {
    this.SetOwner = setOwner;
    this.AutoUpdate = autoUpdate;
    this.CompositionType = compositionType;
    this.FilteredTypes = filteredTypes;
  }
}
