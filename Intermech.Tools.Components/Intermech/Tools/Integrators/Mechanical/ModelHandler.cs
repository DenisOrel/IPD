// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ModelHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class ModelHandler(
  MechanicalDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity modelItem) : DocumentWithArticlesHandler(driver, ctx, modelItem)
{
  protected override PathCollection CollectNewAncillaryFiles()
  {
    PathCollection newAncillaryFiles = base.CollectNewAncillaryFiles();
    IModelDrawingsImportService modelDrawingsService = this.Driver.TryGetModelDrawingsService();
    if (modelDrawingsService != null)
      this.CollectNewDrawings(newAncillaryFiles, modelDrawingsService);
    return newAncillaryFiles;
  }

  private void CollectNewDrawings(
    PathCollection newAncillaryFiles,
    IModelDrawingsImportService modelDrawingsService)
  {
    PathCollection pathCollection = new PathCollection((IEnumerable<string>) FilesSection.CopyAllFiles(this.DocumentFiles));
    pathCollection.AddRange<string>((IEnumerable<string>) newAncillaryFiles);
    PathCollection allDrawingFiles = modelDrawingsService.FindAllDrawingFiles((IEnumerable<string>) pathCollection);
    this.FilterNewAncillaryFiles(allDrawingFiles);
    switch (modelDrawingsService.GetNewDrawingMode())
    {
      case NewDrawingMode.AdditionalModelFile:
        using (IEnumerator<string> enumerator = allDrawingFiles.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            if (UIReport.Enabled)
              UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Components_512"), (object) current));
            newAncillaryFiles.Add(current);
          }
          break;
        }
      case NewDrawingMode.Document:
        using (IEnumerator<string> enumerator = allDrawingFiles.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            if (UIReport.Enabled)
              UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Components_513"), (object) current));
            SectionEntity sectionEntity = this.DriverContext.Database.AddDocument(current);
            this.Driver.AttachDocumentFile(sectionEntity, this.Driver.OpenDocumentFile(sectionEntity, current));
            this.DriverContext.Scheduler.AddTask(this.Driver.CreateDocumentHandler(sectionEntity));
            newAncillaryFiles.Remove(current);
          }
          break;
        }
    }
  }
}
