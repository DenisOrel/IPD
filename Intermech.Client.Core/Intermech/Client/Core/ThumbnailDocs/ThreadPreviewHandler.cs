
// Type: Intermech.Client.Core.ThumbnailDocs.ThreadPreviewHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace Intermech.Client.Core.ThumbnailDocs;

internal class ThreadPreviewHandler
{
  private IPreviewExtract[] intflist;
  private long objID;
  private string filename;

  public ThreadPreviewHandler(string FileName, IPreviewExtract[] list, long ObjID)
  {
    this.filename = FileName;
    this.intflist = list;
    this.objID = ObjID;
  }

  public void Handler(object stateInfo)
  {
    string path1 = "";
    string filename = "";
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.objID, true);
      if (dbObject == null)
        return;
      if (ClientContext.FileVault.WorkArea.IsObjectPublished(this.objID))
      {
        string path2 = Path.Combine(ClientContext.FileVault.WorkArea.AreaPath, this.filename);
        if (File.Exists(path2))
          filename = path2;
      }
      if (filename == "")
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), true);
        for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
        {
          attributeByGuid.Index = index;
          if (this.filename == attributeByGuid.AsString)
            break;
        }
        string tempFileName = ClientContext.FileVault.TempArea.GetTempFileName();
        File.Delete(tempFileName);
        path1 = Path.ChangeExtension(tempFileName, Path.GetExtension(this.filename));
        using (FileStream aDestStream = new FileStream(path1, FileMode.Create, FileAccess.ReadWrite))
          new BlobProcReader(attributeByGuid, 16384 /*0x4000*/, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        filename = path1;
      }
      Image image = (Image) null;
      IPreviewExtract[] intflist = this.intflist;
      for (int index = 0; index < intflist.Length && intflist[index].ExtractPreview(filename, out image) != PreviewExtractStatus.OK; ++index)
        image = (Image) null;
      if (path1 != "")
        File.Delete(path1);
      IDBAttribute dbAttribute = dbObject.GetAttributeByGuid(SystemGUIDs.attributePreview, false);
      if (image != null)
      {
        if (dbAttribute == null)
          dbAttribute = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeID((object) SystemGUIDs.attributePreview), false);
        using (ImChunkedStream imChunkedStream = new ImChunkedStream())
        {
          try
          {
            image.Save((Stream) imChunkedStream, ImageFormat.Png);
            imChunkedStream.Position = 0L;
            BlobInformation blobInfo = new BlobInformation(imChunkedStream.Length, imChunkedStream.Length, DateTime.UtcNow, "preview.png", ArcMethods.NotPacked, "preview.png");
            if (!(dbAttribute is IBlobWriter blobWriter) || !blobWriter.OpenBlob(blobInfo, false))
              return;
            blobWriter.WriteDataBlockEx(imChunkedStream.ToArray(), 0, (int) imChunkedStream.Length);
          }
          catch
          {
          }
          finally
          {
            image.Dispose();
          }
        }
      }
      else
        dbAttribute?.Delete(0L);
    }
  }
}
