// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DefaultAncillaryFilesProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Tools.DataExchange;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

internal sealed class DefaultAncillaryFilesProvider : AncillaryFilesProvider
{
  protected override void DoCollectFiles(SectionEntity documentEntity, PathCollection result)
  {
    int objectType = ObjectSection.GetObjectType(documentEntity);
    string masterFile = FilesSection.GetMasterFile(documentEntity);
    foreach (string additionalFileExt in DocumentTypeSettings.SplitAdditionalFileExts(DocumentTypeSettingsCache.GetSettings(objectType).AdditionalDocumentFileExts))
    {
      string path = Path.ChangeExtension(masterFile, additionalFileExt);
      if (File.Exists(path))
        result.Add(path);
    }
  }
}
