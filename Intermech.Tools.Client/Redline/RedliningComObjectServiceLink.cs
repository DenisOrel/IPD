// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RedliningComObjectServiceLink
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Signs.Interfaces;
using System;

#nullable disable
namespace Intermech.Redline;

internal sealed class RedliningComObjectServiceLink
{
  public RedliningComObjectServiceLink(
    IStartupService startupService,
    ICurrentUserAndRole currentUserService,
    IFileVault fileVaultService,
    IInvokeService invokerService,
    IExternalRedliningEditorService redliningEditorService,
    RedliningIdCache redliningIdCache,
    Lazy<ISignsClientService> signsService,
    Lazy<Intermech.Redline.LastViewedDocumentsService> lastViewedDocumentsService)
  {
    if (startupService == null)
      throw new ArgumentNullException(nameof (startupService));
    if (currentUserService == null)
      throw new ArgumentNullException(nameof (currentUserService));
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (invokerService == null)
      throw new ArgumentNullException(nameof (invokerService));
    if (redliningEditorService == null)
      throw new ArgumentNullException(nameof (redliningEditorService));
    if (redliningIdCache == null)
      throw new ArgumentNullException(nameof (redliningIdCache));
    if (signsService == null)
      throw new ArgumentNullException(nameof (signsService));
    if (lastViewedDocumentsService == null)
      throw new ArgumentNullException(nameof (lastViewedDocumentsService));
    this.StartupService = startupService;
    this.CurrentUserService = currentUserService;
    this.FileVaultService = fileVaultService;
    this.InvokerService = invokerService;
    this.RedliningEditorService = redliningEditorService;
    this.RedliningIdCache = redliningIdCache;
    this.SignsService = signsService;
    this.LastViewedDocumentsService = lastViewedDocumentsService;
  }

  public IStartupService StartupService { get; private set; }

  public ICurrentUserAndRole CurrentUserService { get; private set; }

  public IFileVault FileVaultService { get; private set; }

  public IInvokeService InvokerService { get; private set; }

  public IExternalRedliningEditorService RedliningEditorService { get; private set; }

  public RedliningIdCache RedliningIdCache { get; private set; }

  public Lazy<ISignsClientService> SignsService { get; private set; }

  public Lazy<Intermech.Redline.LastViewedDocumentsService> LastViewedDocumentsService { get; private set; }
}
