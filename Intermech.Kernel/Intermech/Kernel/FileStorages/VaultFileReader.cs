// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.FileStorages.VaultFileReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.IO;


namespace Intermech.Kernel.FileStorages;

public class VaultFileReader : MarshalByRefObject, IVaultFileReader, IDisposable
{
  private Guid sessionGuid;
  protected BinaryReader _Reader;
  protected int _DataBlockSize;
  private BlobAttributeStates _BlobState;
  protected FileInfoStruct _FileStruct;
  private IBlobStoragesPool _StoragesPool;

  public VaultFileReader(Guid sessionGuid) => this.sessionGuid = sessionGuid;

  public BlobInformation OpenBlob(int dataBlockSize, long objectID, int historyID, long storageID)
  {
    BlobInformation blobInformation = new BlobInformation();
    IUserSession sessionById = UserSession.GetSessionByID(this.sessionGuid);
    IBlobStorage storage = this.StoragesPool.GetStorage(storageID, sessionById);
    try
    {
      this._FileStruct = (storage as DiskBlobStorage).GetHistoryFileStructure(historyID, objectID, dataBlockSize > -1);
      blobInformation.ArcMethod = this._FileStruct.ArcMethod;
      blobInformation.BlobID = this._FileStruct.FileID;
      blobInformation.FileName = this._FileStruct.FileName;
      blobInformation.ModifyDate = this._FileStruct.ModifyDate + sessionById.TimeZoneOffset;
      blobInformation.Note = this._FileStruct.Note;
      blobInformation.PackedFileSize = this._FileStruct.PacketFileSize;
      blobInformation.RealFileSize = this._FileStruct.RealFileSize;
      if (dataBlockSize < 0)
      {
        this.BlobState = BlobAttributeStates.Closed;
      }
      else
      {
        this._DataBlockSize = dataBlockSize != 0 ? dataBlockSize : (blobInformation.PackedFileSize <= (long) int.MaxValue ? Convert.ToInt32(blobInformation.PackedFileSize) : int.MaxValue);
        if (this._FileStruct.FileBody != null)
          this._Reader = new BinaryReader(this._FileStruct.FileBody);
        this.BlobState = BlobAttributeStates.OpenedForRead;
      }
    }
    finally
    {
      this.StoragesPool.ReleaseStorage(storage);
    }
    return blobInformation;
  }

  public byte[] ReadDataBlock() => this.ReadDataBlock(0);

  public byte[] ReadDataBlock(int dataBlockSize)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForRead)
      throw new KernelException(LocalizationHolder.rm.GetString("Kernel_988"));
    byte[] numArray = this._FileStruct.FileBody != null ? this._Reader.ReadBytes(dataBlockSize <= 0 ? this._DataBlockSize : dataBlockSize) : new byte[0];
    if (numArray.Length == 0 || this._FileStruct.FileBody.Position == this._FileStruct.FileBody.Length)
      this.CloseBlob();
    return numArray;
  }

  public void CloseBlob()
  {
    if (this.BlobState == BlobAttributeStates.Closed)
      return;
    if (this._Reader != null)
    {
      this._Reader.Close();
      this._Reader = (BinaryReader) null;
    }
    if (this._FileStruct.FileBody != null)
      this._FileStruct.FileBody.Close();
    this.BlobState = BlobAttributeStates.Closed;
  }

  public void Dispose() => this.CloseBlob();

  public BlobAttributeStates BlobState
  {
    get => this._BlobState;
    set
    {
      if (this._BlobState == value)
        return;
      this._BlobState = value == BlobAttributeStates.Closed || this._BlobState == BlobAttributeStates.Closed ? value : throw new KernelExceptionID(4);
    }
  }

  protected BlobStoragesPool StoragesPool
  {
    get
    {
      if (this._StoragesPool == null)
        this._StoragesPool = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
      return this._StoragesPool as BlobStoragesPool;
    }
  }
}
