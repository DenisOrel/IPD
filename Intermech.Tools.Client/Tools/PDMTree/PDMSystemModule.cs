// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystemModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMSystemModule : InitializerModule
{
  private ComServer comServer;
  private IPDMSystemContext pdmSystemContext;
  private ICollection<Type> activatedComClasses;
  private bool isComClassesRegistered;

  public PDMSystemModule(ComServer comServer, IPDMSystemContext pdmSystemContext)
  {
    this.comServer = comServer;
    this.pdmSystemContext = pdmSystemContext;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.pdmSystemContext.PluginManager.LoadComplete += new EventHandler(this.OnPluginLoadComplete);
  }

  private void OnPluginLoadComplete(object sender, EventArgs e) => this.RegisterPDMSystem();

  protected override void DoShutdown()
  {
    this.UnregisterPDMSystem();
    base.DoShutdown();
  }

  private void RegisterPDMSystem()
  {
    if (!this.comServer.IsActive)
      return;
    this.comServer.ComObjectCreated += new EventHandler<ComObjectEventArgs>(this.OnComObjectCreated);
    this.activatedComClasses = this.comServer.ActivateComClasses((ICollection<Type>) new Type[1]
    {
      typeof (PDMSystem)
    }, true);
    this.isComClassesRegistered = true;
  }

  private void UnregisterPDMSystem()
  {
    if (!this.isComClassesRegistered)
      return;
    this.comServer.DeactivateComClasses(this.activatedComClasses);
    this.comServer.ComObjectCreated -= new EventHandler<ComObjectEventArgs>(this.OnComObjectCreated);
    this.activatedComClasses = (ICollection<Type>) null;
    this.isComClassesRegistered = false;
  }

  private void OnComObjectCreated(object sender, ComObjectEventArgs e)
  {
    if (!(e.ComObject is PDMSystem))
      return;
    ((PDMSystem) e.ComObject).PDMSystemContext = this.pdmSystemContext;
  }
}
