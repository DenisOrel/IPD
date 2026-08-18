// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.ModelFileReplaceModule
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using Intermech.Services.IMViewer;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class ModelFileReplaceModule : InitializerModule
{
  private INotificationService notificationService;
  private IIMViewerObjectCreatorService imViewerObjectCreatorService;
  private IClientMetadataCache clientMetadataCache;
  private bool isIntegrationEnabled;

  public ModelFileReplaceModule(
    INotificationService notificationService,
    IIMViewerObjectCreatorService imViewerObjectCreatorService,
    IClientMetadataCache clientMetadataCache)
  {
    this.notificationService = notificationService;
    this.imViewerObjectCreatorService = imViewerObjectCreatorService;
    this.clientMetadataCache = clientMetadataCache;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.isIntegrationEnabled = this.imViewerObjectCreatorService.Settings.EnableIntegration;
    if (!this.isIntegrationEnabled)
      return;
    this.notificationService.Subscribe("FileReplaced", new NotificationEventHandler(this.OnFileReplaced));
  }

  protected override void DoShutdown()
  {
    if (this.isIntegrationEnabled)
      this.notificationService.Unsubscribe("FileReplaced", new NotificationEventHandler(this.OnFileReplaced));
    base.DoShutdown();
  }

  private void OnFileReplaced(object sender, NotificationEventArgs e)
  {
    if (!(e is FileReplacedEventArgs fileReplacedEventArgs) || !this.IsDocumentMasterFile(fileReplacedEventArgs))
      return;
    long elementId = fileReplacedEventArgs.ElementID;
    int elementType = fileReplacedEventArgs.ElementType;
    if (!this.imViewerObjectCreatorService.CanHaveViewerObject(elementType))
      return;
    this.imViewerObjectCreatorService.MakeViewerObjectOutdated(elementId, elementType);
  }

  private bool IsDocumentMasterFile(FileReplacedEventArgs fileReplacedEventArgs)
  {
    return fileReplacedEventArgs.AttributableElement == AttributableElements.Object && fileReplacedEventArgs.AttributeID == this.clientMetadataCache.FileAttributeID && fileReplacedEventArgs.ReplaceFileIndex == 0 && fileReplacedEventArgs.FileType == FileTypes.ftNormal;
  }
}
