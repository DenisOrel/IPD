
// Type: Intermech.Files.CanImportInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;


namespace Intermech.Files;

internal sealed class CanImportInfo
{
  public CanImportInfo(CanImportStatus status)
    : this(status, (FileOrigin) null)
  {
  }

  public CanImportInfo(CanImportStatus status, FileOrigin objectFileOrigin)
  {
    this.Status = status;
    this.ObjectFileOrigin = objectFileOrigin;
  }

  [Conditional("DEBUG")]
  private void CheckCtorParameters(CanImportStatus status, FileOrigin objectFileOrigin)
  {
    switch (status)
    {
      case CanImportStatus.NewFile:
      case CanImportStatus.ExternalFile:
        if (objectFileOrigin == null)
          break;
        throw new ArgumentException("Параметр должен быть равен null.", nameof (objectFileOrigin));
      case CanImportStatus.AlreadyImportedFile:
      case CanImportStatus.AlreadyImportedAndPublishedFile:
        if (objectFileOrigin != null)
          break;
        throw new ArgumentNullException(nameof (objectFileOrigin));
    }
  }

  public CanImportStatus Status { get; private set; }

  public FileOrigin ObjectFileOrigin { get; private set; }
}
