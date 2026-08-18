// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgDependenciesPeek`1
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class DwgDependenciesPeek<TDriver> where TDriver : DocumentCaptureChangesDriver, IDwgDriver
{
  protected readonly TDriver driver;
  protected readonly IApplicationFileTypes fileTypeSvc;

  public DwgDependenciesPeek(TDriver driver)
  {
    this.driver = (object) driver != null ? driver : throw new ArgumentNullException(nameof (driver));
    this.fileTypeSvc = ServiceUtils.GetService<IApplicationFileTypes>((object) driver.Integrator, true);
  }

  public List<DocumentFileData> GetDependencies(DocumentFileData file)
  {
    ObjectSection objectSection = file != null ? file.CustomSections.Get<ObjectSection>() : throw new ArgumentNullException(nameof (file));
    if (objectSection.ObjectType == -1)
      throw new InvalidOperationException($"Document {Path.GetFileName(file.DocumentFilePath)} must already have the definite document type to process dependencies.");
    if (!this.HaveDependencies(objectSection.ObjectType))
      return new List<DocumentFileData>(0);
    List<string> liveXrefs = DwgOperations.GetLiveXRefs(this.driver.Integrator, file.DocumentFilePath);
    DwgOperations.FilterLiveXRefs(file.DocumentFilePath, liveXrefs);
    List<DocumentFileData> dependencies = new List<DocumentFileData>(liveXrefs.Count);
    foreach (string str in liveXrefs)
    {
      if (this.fileTypeSvc.IsApplicationFile(str))
        dependencies.Add(new DocumentFileData(str));
      else
        dependencies.Add(new DocumentFileData(str, true));
    }
    return dependencies;
  }

  protected virtual bool HaveDependencies(int objectType)
  {
    DrawingTypeSettings settings = this.driver.DrawingTypes.FindSettings(objectType);
    return settings != null && settings.XRefMode == XRefMode.Documents;
  }
}
