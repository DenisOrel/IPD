
// Type: Intermech.Files.DeleteAlteredLocalFileAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ControlFlow;
using System;


namespace Intermech.Files;

internal sealed class DeleteAlteredLocalFileAction : IAction
{
  private AlteredFilesService service;
  private string localPath;

  public DeleteAlteredLocalFileAction(AlteredFilesService service, string localPath)
  {
    if (service == null)
      throw new ArgumentNullException(nameof (service));
    if (localPath == null)
      throw new ArgumentNullException(nameof (localPath));
    this.service = service;
    this.localPath = localPath;
  }

  public void Perform() => this.service.RemoveFile(this.localPath);
}
