// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.StorageBlobReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.IO;


namespace Intermech.Kernel;

internal class StorageBlobReader : IBlobReader, IDisposable
{
  private long _BlobID;
  private BlobStorage _Storage;
  private int _DataBlockSize;
  private BinaryReader _Reader;
  private BlobAttributeStates _BlobState;
  private FileInfoStruct _FileStruct;

  public StorageBlobReader(BlobStorage storage, long blobID)
  {
    this._BlobID = blobID;
    this._Storage = storage;
  }

  public BlobInformation OpenBlob(int dataBlockSize)
  {
    if (this.BlobState != BlobAttributeStates.Closed)
      throw new KernelException(sc_13021.ssp_appserver_13022());
    BlobInformation blobInformation = new BlobInformation();
    this._FileStruct = this._Storage.GetFileStruct(this._BlobID, dataBlockSize > -1);
    blobInformation.ArcMethod = this._FileStruct.ArcMethod;
    blobInformation.BlobID = this._FileStruct.FileID;
    blobInformation.FileName = this._FileStruct.FileName;
    blobInformation.ModifyDate = this._FileStruct.ModifyDate;
    blobInformation.Note = this._FileStruct.Note;
    blobInformation.PackedFileSize = this._FileStruct.PacketFileSize;
    blobInformation.RealFileSize = this._FileStruct.RealFileSize;
    blobInformation.Author = this._FileStruct.Author;
    blobInformation.FileType = this._FileStruct.FileType;
    if (dataBlockSize < 0)
    {
      this._BlobState = BlobAttributeStates.Closed;
    }
    else
    {
      this._DataBlockSize = dataBlockSize != 0 ? dataBlockSize : (blobInformation.PackedFileSize <= (long) int.MaxValue ? Convert.ToInt32(blobInformation.PackedFileSize) : int.MaxValue);
      if (this._FileStruct.FileBody != null)
        this._Reader = new BinaryReader(this._FileStruct.FileBody);
      this._BlobState = BlobAttributeStates.OpenedForRead;
    }
    return blobInformation;
  }

  public byte[] ReadDataBlock(int dataBlockSize)
  {
    if (this.BlobState != BlobAttributeStates.OpenedForRead)
      throw new KernelException(sc_13021.ssp_appserver_13023());
    byte[] numArray = this._FileStruct.FileBody != null ? this._Reader.ReadBytes(dataBlockSize <= 0 ? this._DataBlockSize : dataBlockSize) : new byte[0];
    if (numArray.Length == 0 || this._FileStruct.FileBody.Position == this._FileStruct.FileBody.Length)
      this.CloseBlob();
    return numArray;
  }

  public byte[] ReadDataBlock() => this.ReadDataBlock(0);

  public void CloseBlob() => this._BlobState = BlobAttributeStates.Closed;

  public BlobAttributeStates BlobState => this._BlobState;

  public void Dispose()
  {
    this.CloseBlob();
    this._Storage = (BlobStorage) null;
    if (this._Reader == null)
      return;
    this._Reader.Close();
  }
}
