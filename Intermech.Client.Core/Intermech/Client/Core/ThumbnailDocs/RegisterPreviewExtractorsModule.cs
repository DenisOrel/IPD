
// Type: Intermech.Client.Core.ThumbnailDocs.RegisterPreviewExtractorsModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using System;


namespace Intermech.Client.Core.ThumbnailDocs;

internal sealed class RegisterPreviewExtractorsModule : InitializerModule
{
  private IPreviewExtractService service;

  public RegisterPreviewExtractorsModule(IPreviewExtractService service)
  {
    this.service = service != null ? service : throw new ArgumentNullException(nameof (service));
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.service.RegisterExtractor((IPreviewExtract) new PicturePreviewExtract());
    this.service.RegisterExtractor((IPreviewExtract) new SolidWorksExtractPreview());
    this.service.RegisterExtractor((IPreviewExtract) new NXPreviewExrtact());
    this.service.RegisterExtractor((IPreviewExtract) new InventorPreviewExrtact());
    this.service.RegisterExtractor((IPreviewExtract) new SolidEdgePreviewExtract());
    this.service.RegisterExtractor((IPreviewExtract) new CreoPreviewExtract());
    this.service.RegisterExtractor((IPreviewExtract) new KompasPreviewExtract());
  }
}
