// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIDependenciesBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class CIDependenciesBuilder : MechanicalFileDependenciesHandler
{
  private readonly CICaptureChangesDriver driver;
  private readonly IApplicationFileTypes fileTypeSvc;
  private bool collectAssociativeDependencies;
  private bool allowDeferredImport;
  private CIDocumentData documentCustomData;
  private PathCollection depFiles;
  private MasterDocumentsMapping depsMapping;
  private PathCollection assocFiles;
  private CIAssociativeDependencies assocDependenciesSection;

  public CIDependenciesBuilder(
    CICaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext)
    : base((MechanicalDriver) driver, driverContext, ClientContext.FileVault)
  {
    this.driver = driver;
    this.fileTypeSvc = ServiceUtils.GetService<IApplicationFileTypes>((object) driver.Integrator, true);
    this.collectAssociativeDependencies = true;
    this.allowDeferredImport = true;
  }

  public bool CollectAssociativeDependencies
  {
    get => this.collectAssociativeDependencies;
    set => this.collectAssociativeDependencies = value;
  }

  public bool AllowDeferredImport
  {
    get => this.allowDeferredImport;
    set => this.allowDeferredImport = value;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.documentCustomData = this.DocumentFile.CustomSections.Get<CIDocumentData>();
  }

  protected override void DoCleanup()
  {
    base.DoCleanup();
    this.documentCustomData = (CIDocumentData) null;
    this.depFiles = (PathCollection) null;
    this.depsMapping = (MasterDocumentsMapping) null;
    this.assocFiles = (PathCollection) null;
    this.assocDependenciesSection = (CIAssociativeDependencies) null;
  }

  protected override void CollectDependencies()
  {
    this.GetRawDependencies();
    if (this.depFiles.Count <= 0)
      return;
    if (this.DocumentDependencies.Capacity < this.depFiles.Count)
      this.DocumentDependencies.Capacity = this.depFiles.Count;
    this.CollectForeignFiles();
    this.CollectMasterDocuments();
    this.FilterDependencies();
    if (!this.collectAssociativeDependencies || this.DocumentDependencies.Count == 0)
      return;
    switch (this.documentCustomData.Document.DocumentType)
    {
      case CADDocumentType.Undefined:
      case CADDocumentType.Part:
      case CADDocumentType.Assembly:
        this.CollectAssociativeFiles();
        break;
    }
  }

  private void GetRawDependencies()
  {
    Tuple<PathCollection, PathCollection> dependencyFiles = this.documentCustomData.Document.GetDependencyFiles(true);
    this.depFiles = dependencyFiles.Item1;
    this.UnresolvedDependencies.AddRange((IEnumerable<string>) dependencyFiles.Item2);
    Tuple<PathCollection, PathCollection> miscFiles = this.documentCustomData.Document.GetMiscFiles(true);
    this.depFiles.AddRange<string>((IEnumerable<string>) miscFiles.Item1);
    this.UnresolvedDependencies.AddRange<string>((IEnumerable<string>) miscFiles.Item2);
  }

  private void CollectForeignFiles()
  {
    this.DocumentDependencies.AddRange((IEnumerable<DocumentFileData>) CollectionUtils.ConvertAsLinkedList<string, DocumentFileData>((ICollection<string>) CollectionUtils.ExtractAsList<string>((IList<string>) this.depFiles, (Predicate<string>) (fileName => !this.fileTypeSvc.IsApplicationFile(fileName))), (Converter<string, DocumentFileData>) (fileName => new DocumentFileData(fileName, true))));
  }

  private void CollectMasterDocuments()
  {
    this.depsMapping = new MasterDocumentsMapping(this.documentCustomData.Document.CADSystem, this.depFiles.Count);
    this.depsMapping.AddSources((ICollection<string>) this.depFiles);
    foreach (CADDocumentProxy allMasterDocument in this.depsMapping.GetAllMasterDocuments())
    {
      string fullName = allMasterDocument.FullName;
      if (!File.Exists(fullName))
        allMasterDocument.Save();
      switch (CADDocumentHelper.TryReadGlobalPDMFlag((IServiceProvider) this.driver.Integrator, allMasterDocument))
      {
        case 1:
        case 3:
          continue;
        default:
          this.DocumentDependencies.Add(CIDocumentHelper.ReadDocumentData(fullName, allMasterDocument));
          continue;
      }
    }
  }

  /// <summary>
  /// Реализует дополнительную фильтрацию зависимостей со стороны интегратора.
  /// </summary>
  private void FilterDependencies()
  {
    ServiceUtils.GetService<IDataExchangeExtensions>((object) this.driver.Integrator, false)?.CreateDependencyFilterBehavior(this.driver.CADSystem)?.FilterDependencies(this.DocumentDependencies);
  }

  private void CollectAssociativeFiles()
  {
    this.assocFiles = this.documentCustomData.Document.GetAssociativeFiles(false).Item1;
    this.assocDependenciesSection = this.DocumentFile.CustomSections.Get<CIAssociativeDependencies>((CIAssociativeDependencies) null);
    if (this.assocDependenciesSection == null)
    {
      this.assocDependenciesSection = new CIAssociativeDependencies(this.depFiles.Count);
      this.DocumentFile.CustomSections.Set((object) this.assocDependenciesSection);
    }
    foreach (DocumentFileData documentDependency in this.DocumentDependencies)
    {
      bool flag = true;
      if (documentDependency.ForeignFile)
      {
        if (!this.assocFiles.Contains(documentDependency.DocumentFilePath))
          flag = false;
      }
      else
      {
        foreach (string masterDocumentSource in (OrderedList<string>) this.depsMapping.GetMasterDocumentSources(documentDependency.DocumentFilePath))
        {
          if (!this.assocFiles.Contains(masterDocumentSource))
          {
            flag = false;
            break;
          }
        }
      }
      if (flag)
        this.assocDependenciesSection.Files.Add(documentDependency.DocumentFilePath);
    }
  }

  private FileDependencyProcessingParameters GetProcessingParametersNew(
    FileDependencyProcessingData dependency)
  {
    if (dependency.IsNewFile)
    {
      if (CADIntegratorVars.DontImportAssociativeDependencies.Value && this.assocDependenciesSection != null && this.assocDependenciesSection.Files.Contains(dependency.File.DocumentFilePath))
        return FileDependencyProcessingParameters.Ignore;
      if (this.driver.UpdateArticles)
      {
        FileDependencyProcessingParameters withUpdateArticles = this.TryGetProcessingParametersWithUpdateArticles(dependency);
        if (withUpdateArticles != null)
          return withUpdateArticles;
      }
      return FileDependencyProcessingParameters.DeferImport;
    }
    if (this.driver.UpdateArticles && dependency.ObjectId < 0L)
    {
      FileDependencyProcessingParameters withUpdateArticles = this.TryGetProcessingParametersWithUpdateArticles(dependency);
      if (withUpdateArticles != null)
        return withUpdateArticles;
    }
    return FileDependencyProcessingParameters.Ignore;
  }

  private FileDependencyProcessingParameters TryGetProcessingParametersWithUpdateArticles(
    FileDependencyProcessingData dependency)
  {
    if (this.DriverContext.Database.IsEntryPointDocument(this.DocumentEntity))
    {
      if (this.IsDrawingDocumentEntity(this.DocumentEntity))
        return FileDependencyProcessingParameters.Analyse;
      return new FileDependencyProcessingParameters(FileDependencyProcessingMode.Analyze)
      {
        DocumentAnalysisOptions = this.DisableDependenciesProcessingOptions(dependency)
      };
    }
    if (!this.IsDrawingDocumentEntity(this.DriverContext.Database.GetEntryPointDocument(true)))
      return (FileDependencyProcessingParameters) null;
    return new FileDependencyProcessingParameters(FileDependencyProcessingMode.Analyze)
    {
      DocumentAnalysisOptions = this.DisableDependenciesProcessingOptions(dependency)
    };
  }

  private bool IsDrawingDocumentEntity(SectionEntity documentEntity)
  {
    return this.driver.ModelDrawingsService.IsDrawingFileName(FilesSection.GetMasterFile(documentEntity));
  }

  private object DisableDependenciesProcessingOptions(FileDependencyProcessingData dependency)
  {
    return (object) new FilesProcessingOptionsSection()
    {
      EnableDependenciesProcessing = false
    };
  }

  protected override FileDependencyProcessingParameters GetDependencyProcessingParameters(
    FileDependencyProcessingData dependency)
  {
    if (this.AllowDeferredImport)
      return this.GetProcessingParametersNew(dependency);
    FileDependencyProcessingParameters processingParameters = base.GetDependencyProcessingParameters(dependency);
    return processingParameters.Mode != FileDependencyProcessingMode.Analyze || !dependency.IsNewFile || !CADIntegratorVars.DontImportAssociativeDependencies.Value || this.assocDependenciesSection == null || !this.assocDependenciesSection.Files.Contains(dependency.File.DocumentFilePath) ? processingParameters : FileDependencyProcessingParameters.Ignore;
  }

  protected override bool IsSatelliteDependency(FileDependencyProcessingData dependency)
  {
    int num = CADDocumentHelper.TryReadGlobalPDMFlag((IServiceProvider) this.driver.Integrator, dependency.File.CustomSections.Get<CIDocumentData>().Document);
    switch (num)
    {
      case 4:
      case 5:
        if (num == 5)
          dependency.File.CustomSections.Set((object) new CISatelliteModelWithArticles(dependency.File.DocumentFilePath));
        return true;
      default:
        return false;
    }
  }

  protected override void ProcessSatelliteDependency(FileDependencyProcessingData dependency)
  {
    base.ProcessSatelliteDependency(dependency);
    if (!dependency.File.CustomSections.Contains<CISatelliteModelWithArticles>())
      return;
    SectionEntity sectionEntity = new SectionEntity();
    sectionEntity.Sections.CopyFrom((IEnumerable<KeyValuePair<Type, object>>) dependency.File.CustomSections);
    this.DriverContext.Database.Add((IEntity) sectionEntity);
  }
}
