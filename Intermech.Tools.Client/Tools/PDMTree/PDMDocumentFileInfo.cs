// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMDocumentFileInfo
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.IO;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal sealed class PDMDocumentFileInfo
{
  private bool? isMasterFile;

  public PDMDocumentFileInfo(string filePath, long documentId, string masterFilePath)
  {
    this.FilePath = filePath;
    this.DocumentId = documentId;
    this.MasterFilePath = masterFilePath;
  }

  public string FilePath { get; private set; }

  public long DocumentId { get; private set; }

  public string MasterFilePath { get; private set; }

  public bool IsMasterFile
  {
    [DebuggerStepThrough] get
    {
      if (!this.isMasterFile.HasValue)
        this.isMasterFile = new bool?(PathUtils.IsSamePath(this.FilePath, this.MasterFilePath));
      return this.isMasterFile.Value;
    }
  }
}
