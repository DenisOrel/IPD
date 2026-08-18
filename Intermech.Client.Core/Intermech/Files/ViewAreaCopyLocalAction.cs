
// Type: Intermech.Files.ViewAreaCopyLocalAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.IO;


namespace Intermech.Files;

internal sealed class ViewAreaCopyLocalAction : IViewAreaPublishAction
{
  private readonly FileState dbFileState;
  private readonly FileState publishedFileState;
  private readonly string copyFromPath;

  public ViewAreaCopyLocalAction(
    FileState dbFileState,
    FileState publishedFileState,
    string copyFromPath)
  {
    if (dbFileState == null)
      throw new ArgumentNullException(nameof (dbFileState));
    if (publishedFileState == null)
      throw new ArgumentNullException(nameof (publishedFileState));
    if (string.IsNullOrEmpty(copyFromPath))
      throw new ArgumentException();
    this.dbFileState = dbFileState;
    this.publishedFileState = publishedFileState;
    this.copyFromPath = copyFromPath;
  }

  public IFileAttributeAction EmitFileAction(SubArea subArea)
  {
    return (IFileAttributeAction) new CopyLocalFileAction(this.publishedFileState, Path.Combine(subArea.SubareaPath, this.publishedFileState.FileName), this.copyFromPath);
  }

  public FileState DBFileState => this.dbFileState;

  public FileState PublishedFileState => this.publishedFileState;
}
