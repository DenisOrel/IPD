// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.COM.SearchAPIModule
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator.COM;

internal sealed class SearchAPIModule : InitializerModule
{
  private ComServer comServer;
  private Lazy<SearchAPIServiceLink> searchApiServiceLink;
  private ICollection<Type> activatedComClasses;

  public SearchAPIModule(ComServer comServer, Lazy<SearchAPIServiceLink> searchApiServiceLink)
  {
    if (comServer == null)
      throw new ArgumentNullException(nameof (comServer));
    if (searchApiServiceLink == null)
      throw new ArgumentNullException(nameof (searchApiServiceLink));
    this.comServer = comServer;
    this.searchApiServiceLink = searchApiServiceLink;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (!this.comServer.IsActive)
      return;
    this.comServer.ComObjectCreated += new EventHandler<ComObjectEventArgs>(this.OnComObjectCreated);
    this.activatedComClasses = this.comServer.ActivateComClasses((ICollection<Type>) new Type[2]
    {
      typeof (SearchAPI),
      typeof (SpdsAPI)
    }, true);
  }

  protected override void DoShutdown()
  {
    if (this.comServer.IsActive)
    {
      if (this.activatedComClasses != null && this.activatedComClasses.Count != 0)
      {
        this.comServer.DeactivateComClasses(this.activatedComClasses);
        this.activatedComClasses.Clear();
      }
      this.comServer.ComObjectCreated -= new EventHandler<ComObjectEventArgs>(this.OnComObjectCreated);
    }
    base.DoShutdown();
  }

  private void OnComObjectCreated(object sender, ComObjectEventArgs e)
  {
    if (!(e.ComObject is SearchAPIBase))
      return;
    ((SearchAPIBase) e.ComObject).Initialize(this.searchApiServiceLink.Value);
  }
}
