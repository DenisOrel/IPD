// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ProjectDependenciesBuilder
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class ProjectDependenciesBuilder : MechanicalFileDependenciesHandler
{
  private readonly ADMechanicalDriver _driver;
  private readonly List<ADDocument> _documents;
  private AddInProxy _proxy;

  public ProjectDependenciesBuilder(
    ADMechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    List<ADDocument> documents,
    AddInProxy proxy)
    : base((MechanicalDriver) driver, driverContext, ClientContext.FileVault)
  {
    this._driver = driver ?? throw new ArgumentNullException(nameof (driver));
    this._documents = documents ?? throw new ArgumentNullException(nameof (documents));
    this._proxy = proxy ?? throw new ArgumentNullException(nameof (proxy));
  }

  protected override void CollectDependencies()
  {
    base.CollectDependencies();
    foreach (ADDocument document in this._documents)
    {
      DocumentFileData documentFileData = DocumentHelper.ReadDocumentData(document.FullPath, this._proxy);
      if (document.DocumentType == ADDocumentType.SCH && document.AdditionalDocuments != null)
      {
        List<string> sectionObject = new List<string>(document.AdditionalDocuments.Count);
        foreach (ADDocument additionalDocument in document.AdditionalDocuments)
          sectionObject.Add(additionalDocument.FullPath);
        documentFileData.CustomSections.Set((object) sectionObject);
      }
      this.DocumentDependencies.Add(documentFileData);
    }
  }
}
