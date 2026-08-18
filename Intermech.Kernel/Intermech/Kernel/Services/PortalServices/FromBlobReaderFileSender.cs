// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.FromBlobReaderFileSender
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class FromBlobReaderFileSender : FileSender
{
  private readonly IBlobReader _reader;

  public FromBlobReaderFileSender(IBlobReader reader, string unitGuid)
    : base(unitGuid)
  {
    this._reader = reader;
  }

  protected override byte[] ReadData(int startPosition, int packetSize)
  {
    return this._reader.ReadDataBlock(packetSize);
  }
}
