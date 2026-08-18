// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.CADDocumentCopyingInitializerModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying;

internal sealed class CADDocumentCopyingInitializerModule : InitializerModule
{
  private ICompositionCopyingDispatcherService compositionCopyingDispatcherService;
  private CADDocumentCopyingDispatcherHandler compositionCopyingDispatcherHandler;

  public CADDocumentCopyingInitializerModule(
    ICompositionCopyingDispatcherService compositionCopyingDispatcherService,
    CADDocumentCopyingDispatcherHandler compositionCopyingDispatcherHandler)
  {
    if (compositionCopyingDispatcherService == null)
      throw new ArgumentNullException(nameof (compositionCopyingDispatcherService));
    if (compositionCopyingDispatcherHandler == null)
      throw new ArgumentNullException(nameof (compositionCopyingDispatcherHandler));
    this.compositionCopyingDispatcherService = compositionCopyingDispatcherService;
    this.compositionCopyingDispatcherHandler = compositionCopyingDispatcherHandler;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.compositionCopyingDispatcherService.FindBySelectedItems += new EventHandler<FindCompositionCopyingHandlerEventArgs>(this.compositionCopyingDispatcherHandler.FindHandlerBySelectedItems);
  }

  protected override void DoShutdown()
  {
    this.compositionCopyingDispatcherService.FindBySelectedItems -= new EventHandler<FindCompositionCopyingHandlerEventArgs>(this.compositionCopyingDispatcherHandler.FindHandlerBySelectedItems);
    base.DoShutdown();
  }
}
