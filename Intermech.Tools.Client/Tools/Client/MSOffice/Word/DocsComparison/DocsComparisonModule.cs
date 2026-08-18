// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.MSOffice.Word.DocsComparison.DocsComparisonModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Simple;
using Intermech.Tools.MSOffice.Word;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.MSOffice.Word.DocsComparison;

internal class DocsComparisonModule : InitializerModule
{
  private ICompareFilesService compareFilesService;
  private DocsComparisonPlugin docsComparisonPlugin;

  public DocsComparisonModule(
    ICompareFilesService compareFilesService,
    DocsComparisonPlugin docsComparisonPlugin)
  {
    this.compareFilesService = compareFilesService;
    this.docsComparisonPlugin = docsComparisonPlugin;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.AddPluginToFilesCompareService();
  }

  protected override void DoShutdown()
  {
    this.RemovePluginFromFilesCompareService();
    base.DoShutdown();
  }

  private void AddPluginToFilesCompareService()
  {
    IIntegratorSettingsService service = IntegratorServices.GetService<IIntegratorSettingsService>(MSWordConsts.IntegratorRef, true);
    List<int> list;
    try
    {
      list = ((SingleFileSettings) service.GetSettingsObject()).DocumentTypes.Select<GlobalId<int>, int>((Func<GlobalId<int>, int>) (item => item.Id)).ToList<int>();
    }
    catch
    {
      throw;
    }
    if (list == null)
      return;
    this.docsComparisonPlugin.SetTypeIds(list);
    this.compareFilesService.AddPluginToCompareFilesService((ICanCompareObjectsFiles) this.docsComparisonPlugin);
  }

  private void RemovePluginFromFilesCompareService()
  {
    this.compareFilesService.DeletePluginFromCompareFilesService((ICanCompareObjectsFiles) this.docsComparisonPlugin);
  }
}
