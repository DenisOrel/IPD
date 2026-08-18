// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RxmlFileServicesModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Files;
using Intermech.IO;
using System;
using System.IO;

#nullable disable
namespace Intermech.Redline;

internal sealed class RxmlFileServicesModule : InitializerModule
{
  private IFileVault fileVaultService;

  public RxmlFileServicesModule(IFileVault fileVaultService)
  {
    this.fileVaultService = fileVaultService;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.fileVaultService.ReadOnlyLocalFiles.CanControlAttributeEvent += new EventHandler<CanControlFileAttributeEventArgs>(this.CanControlReadOnlyFileAttribute);
  }

  protected override void DoShutdown()
  {
    this.fileVaultService.ReadOnlyLocalFiles.CanControlAttributeEvent -= new EventHandler<CanControlFileAttributeEventArgs>(this.CanControlReadOnlyFileAttribute);
    base.DoShutdown();
  }

  private void CanControlReadOnlyFileAttribute(object sender, CanControlFileAttributeEventArgs e)
  {
    if (!e.CanControl || !PathUtils.IsSamePath(Path.GetExtension(e.RelativeFilePath), ".rxml"))
      return;
    e.CanControl = false;
  }
}
