// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IBlobStorage
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IBlobStorage
{
  long StorageID { get; }

  FileInfoStruct GetFileStruct(long fileID, bool readFileBody);

  FileInfoStruct GetFileStruct(long fileID);

  void SetFileStruct(FileInfoStruct fileStruct);

  bool SetNewFileStruct(FileInfoStruct fileStruct);

  void PrepareTemporaryFile(FileInfoStruct fileStruct);

  void StartTransaction();

  void Commit();

  void Rollback();

  void DeleteFile(long fileID);

  DataTable GetObjectFilesList(long objectID);

  string StorageName { get; }

  string StorageCaption { get; }

  void DeleteStorage();

  IDbManager DataManager { get; }

  void ChangeObjectLinkID(long fileID, long toID);

  void ChangeAttributeID(int attrID, int toAttrID);

  int MaxStorageSize { get; }

  void Clear(long blobID);

  void DeleteTemporaryData();

  void CopyToTemporaryFile(FileInfoStruct fs);

  long FreeSize { get; }

  void Release();

  void Lock();

  bool Locked { get; }
}
