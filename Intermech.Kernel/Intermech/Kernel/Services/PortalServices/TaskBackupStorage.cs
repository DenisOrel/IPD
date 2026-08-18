// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.TaskBackupStorage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using System;
using System.IO;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class TaskBackupStorage : IDisposable
{
  private readonly string _tempPath;
  private readonly BackupWriter _writer;

  public TaskBackupStorage(string rootFolder, Guid taskGuid)
  {
    this._tempPath = TempStorageHelper.CreatePathFromGuid(rootFolder, taskGuid.ToString());
    Directory.CreateDirectory(this._tempPath);
    this._writer = new BackupWriter(this._tempPath);
  }

  public IBackupWriter Writer => (IBackupWriter) this._writer;

  public void SaveToFile(IDBAttribute attributeTaskFile)
  {
    this._writer.Close();
    if (!attributeTaskFile.IsNull)
      attributeTaskFile.AddValue((object) null);
    IBlobWriter writer = attributeTaskFile as IBlobWriter;
    FileInfo fileInfo = new FileInfo(Path.Combine(this._tempPath, BackupConsts.DataFileName));
    writer.OpenBlob(new BlobInformation(fileInfo.Length, fileInfo.Length, DateTime.Now, BackupConsts.DataFileName, ArcMethods.NotPacked, BackupConsts.DataFileName), false);
    this.Write(writer, new BinaryReader((Stream) File.OpenRead(Path.Combine(this._tempPath, BackupConsts.DataFileName)), Encoding.UTF8));
  }

  private void Write(IBlobWriter writer, BinaryReader reader)
  {
    try
    {
      byte[] numArray1 = new byte[Consts.BlobTransferBufferLength];
      int length;
      while ((length = reader.Read(numArray1, 0, Consts.BlobTransferBufferLength)) > 0)
      {
        if (length < Consts.BlobTransferBufferLength)
        {
          byte[] numArray2 = new byte[length];
          Array.Copy((Array) numArray1, (Array) numArray2, length);
          writer.WriteDataBlock(numArray2);
        }
        else
          writer.WriteDataBlock(numArray1);
      }
    }
    finally
    {
      reader.Close();
    }
  }

  public void Dispose()
  {
    if (this._writer != null)
      this._writer.Close();
    Directory.Delete(this._tempPath, true);
  }
}
