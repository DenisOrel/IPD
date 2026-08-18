// Decompiled with JetBrains decompiler
// Type: Intermech.Files.PublishedFile
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.IO;

#nullable disable
namespace Intermech.Files;

public sealed class PublishedFile
{
  private string fullName;
  private FileState fileState;
  private long blobId;

  public PublishedFile(string fullName, FileState fileState, long blobId)
  {
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException();
    if (fileState == null)
      throw new ArgumentNullException();
    if (blobId == 0L)
      throw new ArgumentException();
    this.fullName = File.Exists(fullName) ? fullName : throw new FileNotFoundException(LocalizationHolder.rm.GetString("Client.Core_1278"), fullName);
    this.fileState = fileState;
    this.blobId = blobId;
  }

  public string FullName => this.fullName;

  public FileState FileState => this.fileState;

  public long BlobId => this.blobId;
}
