// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.RedliningComObjectModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Redline;

internal sealed class RedliningComObjectModule : InitializerModule
{
  private ComServer comServer;
  private Lazy<RedliningComObjectServiceLink> redliningServiceLink;
  private ICollection<Type> activatedComClasses;

  public RedliningComObjectModule(
    ComServer comServer,
    Lazy<RedliningComObjectServiceLink> redliningServiceLink)
  {
    this.comServer = comServer;
    this.redliningServiceLink = redliningServiceLink;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (!this.comServer.IsActive)
      return;
    this.comServer.ComObjectCreated += new EventHandler<ComObjectEventArgs>(this.OnComObjectCreated);
    this.activatedComClasses = this.comServer.ActivateComClasses((ICollection<Type>) new Type[1]
    {
      typeof (RedliningComObject)
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
    if (!(e.ComObject is RedliningComObject comObject))
      return;
    comObject.Initialize(this.redliningServiceLink.Value);
  }
}
