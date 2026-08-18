// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADArticleDocumentationService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADArticleDocumentationService : ArticleDocumentationService
{
  private FileTypeService _fileTypeService;
  private ADIntegratorSettings _settings;

  public ADArticleDocumentationService(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    IFileVault fileVault,
    FileTypeService fileTypeService,
    ADIntegratorSettings settings)
    : base(driver, driverContext, fileVault)
  {
    this._fileTypeService = fileTypeService ?? throw new ArgumentNullException(nameof (fileTypeService));
    this._settings = settings;
  }

  public override List<SectionEntity> GetDocuments(SectionEntity articleItem)
  {
    List<SectionEntity> documents = base.GetDocuments(articleItem);
    if (articleItem.Sections.Get<ElectricalArticleCache>().ArticleType == ArticleTypes.Component)
      return documents;
    SectionEntity articleMainDocument = this.Driver.MechanicalOperations.Articles.TryGetArticleMainDocument(articleItem);
    if (articleMainDocument != null)
      this.CollectDocuments(articleMainDocument, documents);
    return documents;
  }

  private void CollectDocuments(SectionEntity modelItem, List<SectionEntity> result)
  {
    AddInProxy proxy = modelItem.Sections.Get<AddInProxy>();
    FilesSection filesSection = modelItem.Sections.Get<FilesSection>();
    IADProject project = ApiHelper.GetProject(proxy.AddIn, filesSection.MasterFile);
    using (ADClientSponsor adClientSponsor = new ADClientSponsor())
    {
      adClientSponsor.Register((object) project);
      foreach (ADDocument projectDocument in DocumentHelper.GetProjectDocuments(project.GetDocuments(false), this._fileTypeService, proxy))
        this.TryAddDocument(projectDocument.FullPath, false, result);
    }
  }
}
