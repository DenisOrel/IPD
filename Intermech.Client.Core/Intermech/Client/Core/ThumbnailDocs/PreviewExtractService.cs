
// Type: Intermech.Client.Core.ThumbnailDocs.PreviewExtractService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;


namespace Intermech.Client.Core.ThumbnailDocs;

internal class PreviewExtractService : IPreviewExtractService
{
  private List<IPreviewExtract> _list = new List<IPreviewExtract>();
  private NotificationEventHandler _notifyHandler;

  public PreviewExtractService(INotificationService notificationService)
  {
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    this._notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
    notificationService.Subscribe("ObjectsCheckedIn", this._notifyHandler);
  }

  private void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
      return;
    for (int index = 0; index < objectsEventArgs.ObjectIDs.Count; ++index)
    {
      int objectTypeId = objectsEventArgs.ObjectTypeIDs[index];
      long num = Math.Abs(objectsEventArgs.ObjectIDs[index]);
      if (objectTypeId == -1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          objectTypeId = sessionKeeper.Session.GetObjectInfo(num).ObjectTypeID;
      }
      if (MetaDataHelper.IsObjectTypeChildOf(objectTypeId, MetaDataHelper.GetObjectTypeID(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"))))
      {
        string filename = ClientContext.FileVault.DBFilesInfo.GetMasterFileName(num, false);
        if (!string.IsNullOrEmpty(filename))
        {
          IPreviewExtract[] array = this._list.Where<IPreviewExtract>((Func<IPreviewExtract, bool>) (x => x.Supports(filename))).ToArray<IPreviewExtract>();
          if (array.Length != 0)
            ThreadPool.QueueUserWorkItem(new WaitCallback(new ThreadPreviewHandler(filename, array, num).Handler));
        }
      }
    }
  }

  public void RegisterExtractor(IPreviewExtract extractor) => this._list.Add(extractor);

  public string GetAllSupportExtensions()
  {
    return string.Join(",", this._list.Select<IPreviewExtract, string>((Func<IPreviewExtract, string>) (x => x.GetSupportExtensions())).ToArray<string>());
  }

  public Image GetImage(string fileFullName)
  {
    foreach (IPreviewExtract previewExtract in this._list)
    {
      Image image;
      if (previewExtract.Supports(fileFullName) && previewExtract.ExtractPreview(fileFullName, out image) == PreviewExtractStatus.OK)
        return image;
    }
    return (Image) null;
  }
}
