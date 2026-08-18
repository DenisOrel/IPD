
// Type: Intermech.Files.ViewAreaDownloadAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.IO;


namespace Intermech.Files;

internal sealed class ViewAreaDownloadAction : IViewAreaPublishAction
{
  private readonly FileState fileState;

  public ViewAreaDownloadAction(FileState fileState)
  {
    this.fileState = fileState != null ? fileState : throw new ArgumentNullException(nameof (fileState));
  }

  public IFileAttributeAction EmitFileAction(SubArea subArea)
  {
    return (IFileAttributeAction) new DownloadFileAction(this.fileState, Path.Combine(subArea.SubareaPath, this.fileState.FileName));
  }

  public FileState DBFileState => this.fileState;

  public FileState PublishedFileState => this.fileState;
}
