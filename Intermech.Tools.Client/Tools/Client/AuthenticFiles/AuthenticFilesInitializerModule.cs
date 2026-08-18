// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.AuthenticFiles.AuthenticFilesInitializerModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using System;

#nullable disable
namespace Intermech.Tools.Client.AuthenticFiles;

internal sealed class AuthenticFilesInitializerModule : InitializerModule
{
  private readonly CreateAuthenticFileExtender createAuthenticFileExtender;
  private readonly UpdateAuthenticFileOnSaveChangesExtender updateAuthenticPdfsOnSaveExtender;

  public AuthenticFilesInitializerModule(
    CreateAuthenticFileExtender createAuthenticFileExtender,
    UpdateAuthenticFileOnSaveChangesExtender updateAuthenticFileOnSave)
  {
    if (createAuthenticFileExtender == null)
      throw new ArgumentNullException(nameof (createAuthenticFileExtender));
    if (updateAuthenticFileOnSave == null)
      throw new ArgumentNullException(nameof (updateAuthenticFileOnSave));
    this.createAuthenticFileExtender = createAuthenticFileExtender;
    this.updateAuthenticPdfsOnSaveExtender = updateAuthenticFileOnSave;
    this.updateAuthenticPdfsOnSaveExtender.AuthenticFileExtension = ".pdf";
    this.updateAuthenticPdfsOnSaveExtender.SaveChangesModeFilter = (Predicate<SaveChangesMode>) (mode => mode == SaveChangesMode.Checkin);
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.createAuthenticFileExtender.Enabled = true;
    this.updateAuthenticPdfsOnSaveExtender.Enabled = true;
  }

  protected override void DoShutdown()
  {
    if (this.updateAuthenticPdfsOnSaveExtender != null)
      this.updateAuthenticPdfsOnSaveExtender.Enabled = false;
    if (this.createAuthenticFileExtender != null)
      this.createAuthenticFileExtender.Enabled = false;
    base.DoShutdown();
  }
}
