// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgCreator.DwgCreatorModule
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.ApplicationModel;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgCreator;

internal sealed class DwgCreatorModule : InitializerModule
{
  private DwgCreatorProvider dwgCreatorProvider;

  public DwgCreatorModule(DwgCreatorProvider dwgCreatorProvider)
  {
    this.dwgCreatorProvider = dwgCreatorProvider != null ? dwgCreatorProvider : throw new ArgumentNullException(nameof (dwgCreatorProvider));
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.dwgCreatorProvider.Enabled = true;
  }

  protected override void DoShutdown()
  {
    this.dwgCreatorProvider.Enabled = false;
    base.DoShutdown();
  }
}
