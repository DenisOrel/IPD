// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.FileInfoStruct
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.IO;
using System;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Server;

public class FileInfoStruct
{
  private long _FileID;
  private long _RealFileSize;
  public long PacketFileSize;
  private DateTime _ModifyDate;
  public string _FileName = string.Empty;
  public Stream FileBody;
  public ArcMethods ArcMethod;
  public long ObjectLinkID;
  public string _Note;
  private string _IsolatedFileName;
  public int AttributeID;
  public FileTypes FileType;
  public long Author;
  private bool _IsolatedCacheMode = true;

  public bool IsolatedCacheMode
  {
    get => this._IsolatedCacheMode;
    set => this._IsolatedCacheMode = value;
  }

  public string Note
  {
    get => this._Note;
    set
    {
      if (value != null && value.Length > Intermech.Consts.MaxStorageNoteValueSize)
        this._Note = value.Substring(0, Intermech.Consts.MaxStorageNoteValueSize);
      else
        this._Note = value;
    }
  }

  public long FileID
  {
    get => this._FileID;
    set
    {
      if (this._FileID == value)
        return;
      this._FileID = value;
      if (!this.IsolatedCacheMode)
        return;
      this._IsolatedFileName = (string) null;
    }
  }

  public string FileName
  {
    get => this._FileName;
    set
    {
      if (value.Length > Intermech.Consts.MaxObjectNameLength)
        throw new KernelExceptionID(422, (object) Intermech.Consts.MaxObjectNameLength, (object) value);
      this._FileName = value;
    }
  }

  public long RealFileSize
  {
    get => this._RealFileSize;
    set
    {
      if (this._RealFileSize == value)
        return;
      this._RealFileSize = value;
      if (!this.IsolatedCacheMode)
        return;
      this._IsolatedFileName = (string) null;
    }
  }

  public DateTime ModifyDate
  {
    get => this._ModifyDate;
    set
    {
      if (!(this._ModifyDate != value))
        return;
      this._ModifyDate = value;
      if (!this.IsolatedCacheMode)
        return;
      this._IsolatedFileName = (string) null;
    }
  }

  public string IsolatedFileName
  {
    set => this._IsolatedFileName = value;
  }

  public string GetIsolatedFileName(FilesStorage storage)
  {
    if (this._IsolatedFileName == null)
      this._IsolatedFileName = storage.GetFullFileName($"{this.FileID}_{this.PacketFileSize}_{this.ModifyDate.Ticks}");
    return this._IsolatedFileName;
  }

  public string GetIsolatedFileName(FilesStorage storage, bool withRandomID)
  {
    string isolatedFileName = this.GetIsolatedFileName(storage);
    if (withRandomID)
      isolatedFileName = $"{isolatedFileName}_{(object) FileInfoStructHelper.GetBlobStorageRandom(99999)}";
    return isolatedFileName;
  }

  public byte[] GetFileBytes()
  {
    this.FileBody.Position = 0L;
    BinaryReader binaryReader = new BinaryReader(this.FileBody);
    byte[] fileBytes = binaryReader.ReadBytes(Convert.ToInt32(this.FileBody.Length));
    binaryReader.Close();
    return fileBytes;
  }
}
