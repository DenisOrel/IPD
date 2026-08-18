// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.IReadWorker
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

#nullable disable
namespace Intermech.Vault.Interfaces;

public interface IReadWorker
{
  int ReadBlock(ref byte[] dataBlock, int dataLength);

  void Close();
}
