// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.BlobContainer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Kernel;

public class BlobContainer
{
  private byte[] _BlobBody;
  private BlobInformation _BlobInfo;
  private int _Position;

  public BlobContainer(BlobInformation blobInformation)
  {
    this._BlobInfo = blobInformation;
    this._BlobBody = new byte[blobInformation.PackedFileSize];
  }

  public int Position => this._Position;

  public byte[] BlobBody => this._BlobBody;

  public BlobInformation BlobInfo => this._BlobInfo;

  public void WriteDataBlockEx(byte[] data, int index, int length)
  {
    Array.Copy((Array) data, index, (Array) this._BlobBody, this._Position, length);
    this._Position += length;
  }

  public byte[] ReadDataBlock(int dataBlockSize)
  {
    byte[] destinationArray = new byte[dataBlockSize];
    Array.Copy((Array) this.BlobBody, this._Position, (Array) destinationArray, 0, dataBlockSize);
    this._Position += dataBlockSize;
    return destinationArray;
  }
}
