// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.UI.FileError
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive.UI;

internal sealed class FileError
{
  private readonly string fileName;
  private readonly string error;

  public FileError(string fileName, string error)
  {
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(error))
      throw new ArgumentException();
    this.fileName = fileName;
    this.error = error;
  }

  public string FileName => this.fileName;

  public string Error => this.error;
}
