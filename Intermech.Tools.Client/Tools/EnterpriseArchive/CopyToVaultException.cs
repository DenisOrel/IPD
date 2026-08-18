// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.CopyToVaultException
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class CopyToVaultException : Exception
{
  private readonly string fileName;

  public CopyToVaultException(string fileName, string message, Exception innerException)
    : base(message, innerException)
  {
    this.fileName = !string.IsNullOrEmpty(fileName) ? fileName : throw new ArgumentException();
  }

  public string FileName => this.fileName;
}
