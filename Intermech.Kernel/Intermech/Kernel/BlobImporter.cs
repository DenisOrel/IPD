// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.BlobImporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.IO;


namespace Intermech.Kernel;

public class BlobImporter : LongLifeObject, IBlobImporter
{
  private BlobStoragesPool BlobPool;

  public BlobImporter(BlobStoragesPool blobPool)
  {
    this.BlobPool = blobPool != null ? blobPool : throw new ArgumentNullException(nameof (blobPool));
  }

  public long AddBlob(Guid sessionGuid, BlobInformation4Import blobInfo)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_12998.ssp_appserver_12999(1735382122));
    long num = sessionById.DataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", sessionById.DataManager);
    FileInfoStruct fileStruct = new FileInfoStruct();
    fileStruct.ArcMethod = blobInfo.ArcMethod;
    fileStruct.FileID = num;
    fileStruct.FileName = blobInfo.FileName;
    fileStruct.Note = blobInfo.Note;
    fileStruct.ObjectLinkID = blobInfo.ObjectID;
    fileStruct.PacketFileSize = blobInfo.PackedFileSize;
    fileStruct.RealFileSize = blobInfo.RealFileSize;
    fileStruct.AttributeID = blobInfo.AttributeID;
    fileStruct.FileType = blobInfo.FileType;
    fileStruct.Author = blobInfo.Author;
    fileStruct.ModifyDate = blobInfo.ModifyDate;
    using (fileStruct.FileBody = (Stream) new FileStream(blobInfo.LocalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
    {
      IBlobStorage storage = this.BlobPool.GetStorage(this.BlobPool.GetActiveStorageID((IUserSession) sessionById), (IUserSession) sessionById);
      try
      {
        sessionById.StartTransaction();
        try
        {
          storage.SetNewFileStruct(fileStruct);
          sessionById.Commit();
        }
        catch
        {
          sessionById.Rollback();
          throw;
        }
      }
      finally
      {
        this.BlobPool.ReleaseStorage(storage);
      }
    }
    return num;
  }
}
