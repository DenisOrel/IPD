// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.BackupWriter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using System.IO;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class BackupWriter : IBackupWriter
{
  private BinaryWriter _blobsWriter;
  private readonly string _tempPath;

  public BackupWriter(string tempPath) => this._tempPath = tempPath;

  private BinaryWriter BlobsWriter
  {
    get
    {
      if (this._blobsWriter == null)
        this._blobsWriter = new BinaryWriter((Stream) File.Create(Path.Combine(this._tempPath, BackupConsts.DataFileName)), Encoding.UTF8);
      return this._blobsWriter;
    }
  }

  public void WriteBlob(byte[] buffer) => this.BlobsWriter.Write(buffer);

  private void CloseWriter(BinaryWriter writer) => writer?.Close();

  public void Close() => this.CloseWriter(this._blobsWriter);
}
