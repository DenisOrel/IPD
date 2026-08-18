// Decompiled with JetBrains decompiler
// Type: Intermech.MSOffice.MSOfficeIndexersModule
// Assembly: Intermech.MSOffice.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D19FBC55-F588-4D57-844C-DE1B05B4B055
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MSOffice.Server.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.GlobalIndex;

#nullable disable
namespace Intermech.MSOffice;

internal sealed class MSOfficeIndexersModule : InitializerModule
{
  private IGlobalIndexService globalIndexSvc;
  private MSOfficeDocumentIndexer docIndexer;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.globalIndexSvc = (IGlobalIndexService) ServerServices.GetService(typeof (IGlobalIndexService));
    this.docIndexer = new MSOfficeDocumentIndexer();
    this.globalIndexSvc.RegisterFileConverter((IIndexerFileConverter) this.docIndexer);
  }

  protected override void DoShutdown()
  {
    base.DoShutdown();
    if (this.docIndexer != null)
      this.docIndexer = (MSOfficeDocumentIndexer) null;
    this.globalIndexSvc = (IGlobalIndexService) null;
  }
}
