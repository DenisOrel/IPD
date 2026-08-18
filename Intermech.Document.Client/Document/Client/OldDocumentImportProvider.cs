// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.OldDocumentImportProvider
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Document.Client;

internal class OldDocumentImportProvider
{
  private static List<string> docExtensions;
  private static List<string> templateExtensions;
  private static IServiceProvider serviceProvider;

  internal static void Init(IServiceProvider provider)
  {
    OldDocumentImportProvider.serviceProvider = provider;
    ClientContext.FileImporter.FileProbe += new EventHandler<FileProbeEventArgs>(OldDocumentImportProvider.importService_FileProbe);
    OldDocumentImportProvider.docExtensions = new List<string>();
    OldDocumentImportProvider.templateExtensions = new List<string>();
    OldDocumentImportProvider.docExtensions.Add(".imdx");
    OldDocumentImportProvider.docExtensions.Add(".zimd");
    OldDocumentImportProvider.docExtensions.Add(".imd");
    OldDocumentImportProvider.docExtensions.Add(".revx");
    OldDocumentImportProvider.docExtensions.Add(".rev");
    OldDocumentImportProvider.docExtensions.Add(".cc");
    OldDocumentImportProvider.docExtensions.Add(".rep");
    OldDocumentImportProvider.templateExtensions.Add(".bln");
  }

  private static void importService_FileProbe(object sender, FileProbeEventArgs e)
  {
    string lower = e.FileInfo.Extension.ToLower();
    if (!OldDocumentImportProvider.templateExtensions.Contains(lower) && !OldDocumentImportProvider.docExtensions.Contains(lower))
      return;
    e.ImportHandler = new ImportFileHandler(OldDocumentImportProvider.OldDocumentImportHandler);
  }

  private static FileImportResult OldDocumentImportHandler(
    string fullPath,
    FileImportOptions importOptions)
  {
    IObjectCreatorService service = OldDocumentImportProvider.serviceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    string lower = Path.GetExtension(fullPath).ToLower();
    long num = -1;
    List<int> intList = new List<int>();
    if (OldDocumentImportProvider.docExtensions.Contains(lower))
    {
      intList = MetaDataHelper.GetObjectTypeChildrenID(DocIDCache.ObjType_Document);
      intList.Add(DocIDCache.ObjType_Document);
    }
    if (OldDocumentImportProvider.templateExtensions.Contains(lower))
    {
      intList = MetaDataHelper.GetObjectTypeChildrenID(DocIDCache.ObjType_ImDocTemplate);
      intList.Add(DocIDCache.ObjType_ImDocTemplate);
    }
    if (intList != null && intList.Count > 0)
    {
      num = service.CreateObjectByTypeDialog(intList.ToArray());
      if (num != -1L && ImDocument.LoadFromFile(fullPath, out DocumentFileType _, false) is ImDocument document)
        DocumentEditorPlugin.SaveImDocumentObjectFile(num, document, Path.GetFileNameWithoutExtension(fullPath) + ".imdx", 0, false);
    }
    return (FileImportResult) new FileImportResult.Success(fullPath, num);
  }
}
