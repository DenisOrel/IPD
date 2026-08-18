// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DAssemblyDocumentApiService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DAssemblyDocumentApiService(
  K3DCaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext,
  K3DCADInterfaceService cadInterfaceService) : Drawing2DGenericDocumentApiService(driver, driverContext, cadInterfaceService)
{
  private K3DCaptureChangesDriver K3DDriver
  {
    [DebuggerStepThrough] get => (K3DCaptureChangesDriver) this.CIDriver;
  }

  public override ICollection<InitialArticleData> ReadArticles(SectionEntity documentItem)
  {
    ICollection<InitialArticleData> result = documentItem != null ? base.ReadArticles(documentItem) : throw new ArgumentNullException(nameof (documentItem));
    this.EmitAssemblyComponentArticles(documentItem, result);
    return result;
  }

  private void EmitAssemblyComponentArticles(
    SectionEntity docItem,
    ICollection<InitialArticleData> result)
  {
    List<AssemblyStructureRecord> articleStructureCached = this.CommonOperations.GetArticleStructureCached(docItem);
    OrderedList<string> orderedList = new OrderedList<string>(articleStructureCached.Count);
    foreach (AssemblyStructureRecord assemblyStructureRecord in articleStructureCached)
    {
      if (assemblyStructureRecord.ComponentConfiguration.IsInMemory && PathUtils.IsSamePath(Path.GetFileName(assemblyStructureRecord.ComponentMasterFile), "VirtualComponents.m3d"))
      {
        string str = this.CommonOperations.MakeArticleKey((string) assemblyStructureRecord.ComponentConfiguration.Name, assemblyStructureRecord.ComponentMasterFile);
        if (!orderedList.Contains(str))
        {
          InitialArticleData initialArticleData = this.EmitConfigurationArticle(assemblyStructureRecord.ComponentConfiguration, assemblyStructureRecord.ComponentMasterFile, ArticleInitialDocumentType.None);
          this.K3DDriver.Drawing2DOperations.AddCustomArticleData(initialArticleData.CustomSections, Drawing2DArticleKind.ComponentArticle);
          result.Add(initialArticleData);
          orderedList.Add(str);
        }
      }
    }
  }
}
