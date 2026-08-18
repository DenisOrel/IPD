// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleDocumentationService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class CIArticleDocumentationService : ArticleDocumentationService
{
  private IModelDrawingsService modelDrawingsService;
  private List<int> drawingTypes;

  public CIArticleDocumentationService(
    CICaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext,
    IFileVault fileVault,
    IModelDrawingsService modelDrawingsService)
    : base((MechanicalDriver) driver, driverContext, fileVault)
  {
    this.modelDrawingsService = modelDrawingsService != null ? modelDrawingsService : throw new ArgumentNullException(nameof (modelDrawingsService));
  }

  private CICaptureChangesDriver CIDriver
  {
    [DebuggerStepThrough] get => (CICaptureChangesDriver) this.Driver;
  }

  public override List<SectionEntity> GetDocuments(SectionEntity articleItem)
  {
    List<SectionEntity> documents = base.GetDocuments(articleItem);
    SectionEntity articleMainDocument = this.CIDriver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
      this.CollectModelDrawings(articleItem, articleMainDocument, documents);
    return documents;
  }

  private void CollectModelDrawings(
    SectionEntity articleItem,
    SectionEntity modelItem,
    List<SectionEntity> documents)
  {
    PathCollection modelFiles = FilesSection.CopyAllFiles(modelItem);
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) this.DriverContext.Database.Query((IQueryCondition) new BinaryCondition((object) ObjectSection.ObjectTypeRef, BinaryOperator.In, (object) this.DrawingTypes)))
    {
      FilesSection filesSection = sectionEntity.Sections.Get<FilesSection>((FilesSection) null);
      if (filesSection != null && this.IsTargetDrawingFile(filesSection.MasterFile, (IEnumerable<string>) modelFiles))
        documents.Add(sectionEntity);
    }
    ObjectSection objectSection = modelItem.Sections.Get<ObjectSection>();
    if (objectSection.NewObject)
      return;
    foreach (long documentDrawing in DBDocumentHelper.FindDocumentDrawings(objectSection.ObjectId, VersionsRuleSources.GetEditorRule(), (IList<int>) this.drawingTypes))
    {
      if (this.IsTargetDrawingFile(Path.GetFullPath(Path.Combine(this.FileVault.WorkArea.AreaPath, this.FileVault.DBFilesInfo.GetMasterFileName(documentDrawing, true))), (IEnumerable<string>) modelFiles))
        this.TryAddDocument(documentDrawing, true, documents);
    }
  }

  private bool IsTargetDrawingFile(string drawingFileName, IEnumerable<string> modelFiles)
  {
    return this.modelDrawingsService.FindSourceModelFile(modelFiles, drawingFileName) != null;
  }

  protected override void MakeRelationAttributes(
    SectionEntity articleItem,
    SectionEntity documentItem,
    ValueBag attributes)
  {
    this.IsDrawingDocument(documentItem);
    CIArticleData ciArticleData = articleItem.Sections.Get<CIArticleData>((CIArticleData) null);
    if (ciArticleData != null)
      attributes.AddWithFlag((StringKey) IDCache.Default.CADConfigurationName.Text, (object) (string) ciArticleData.Configuration.Name, NamedFlags.ReadOnly);
    base.MakeRelationAttributes(articleItem, documentItem, attributes);
  }

  private bool IsDrawingDocument(SectionEntity documentItem)
  {
    MechanicalDocumentKind? mechanicalDocumentKind = this.Driver.TryGetMechanicalDocumentKind(documentItem);
    return mechanicalDocumentKind.HasValue && (mechanicalDocumentKind.Value == MechanicalDocumentKind.AssemblyDrawing || mechanicalDocumentKind.Value == MechanicalDocumentKind.PartDrawing) || this.DrawingTypes.Contains(ObjectSection.GetObjectType(documentItem));
  }

  private List<int> DrawingTypes
  {
    get
    {
      if (this.drawingTypes == null)
      {
        CADSettings integratorSettings = this.CIDriver.IntegratorSettings;
        this.drawingTypes = new List<int>(16 /*0x10*/);
        this.drawingTypes.AddRange((IEnumerable<int>) integratorSettings.FileDocumentGroups.FindByName("AssemblyDrawing", true).AsIdList());
        this.drawingTypes.AddRange((IEnumerable<int>) integratorSettings.FileDocumentGroups.FindByName("PartDrawing", true).AsIdList());
      }
      return this.drawingTypes;
    }
  }
}
