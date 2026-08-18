
// Type: Intermech.Files.SubArea
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.IO;


namespace Intermech.Files;

internal sealed class SubArea : IDisposable
{
  private readonly ViewAreaService area;
  private readonly string directoryName;
  private readonly string indexFilePath;
  private readonly string subareaPath;
  private ViewAreaIndexFile indexFile;
  private ViewAreaIndexService indexFileService;

  public SubArea(ViewAreaService area, string directoryName, string indexFilePath)
  {
    if (area == null)
      throw new ArgumentNullException(nameof (area));
    if (string.IsNullOrEmpty(directoryName))
      throw new ArgumentException();
    if (string.IsNullOrEmpty(indexFilePath))
      throw new ArgumentException();
    this.area = area;
    this.directoryName = directoryName;
    this.subareaPath = Path.Combine(this.area.AreaPath, directoryName);
    this.indexFilePath = indexFilePath;
    this.indexFile = new ViewAreaIndexFile(this.indexFilePath, 1024 /*0x0400*/, false);
    this.indexFileService = new ViewAreaIndexService(this.indexFile);
  }

  public void Dispose()
  {
    this.indexFileService = (ViewAreaIndexService) null;
    this.indexFile.ReleaseDatabase();
  }

  public void PublishFiles(ICollection<FileState> files)
  {
    this.indexFileService.BatchAppend(files);
  }

  public void UnpublishAll() => this.indexFileService.BatchRemoveAll();

  public void UnpublishFiles(ICollection<string> files) => this.indexFileService.BatchRemove(files);

  public FileState FindPublishedState(string fileName) => this.indexFileService.Find(fileName);

  public string SubareaPath => this.subareaPath;

  public string DirectoryName => this.directoryName;

  public string IndexFilePath => this.indexFilePath;
}
