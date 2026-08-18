// Decompiled with JetBrains decompiler
// Type: Intermech.Checksums.LengthedCryptoStream
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.IO;
using System.Security.Cryptography;


namespace Intermech.Checksums;

internal class LengthedCryptoStream(
  Stream stream,
  ICryptoTransform transform,
  CryptoStreamMode mode) : CryptoStream(stream, transform, mode)
{
  public override long Length => 0;
}
