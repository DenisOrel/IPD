// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.FromBytesFileSender
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel.Services.PortalServices;

internal class FromBytesFileSender : FileSender
{
  public byte[] Data { get; set; }

  public FromBytesFileSender(byte[] data, string unitGuid)
    : base(unitGuid)
  {
    this.Data = data;
  }

  protected override byte[] ReadData(int startPosition, int packetSize)
  {
    byte[] destinationArray = new byte[packetSize];
    Array.Copy((Array) this.Data, startPosition, (Array) destinationArray, 0, packetSize);
    return destinationArray;
  }
}
