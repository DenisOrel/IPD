
// Type: Intermech.Services.PrepareForViewDocumentFilesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;


namespace Intermech.Services;

/// <summary>
/// Класс сервиса для подготовки файлов документов IPS к просмотру или печати во внешнем приложении.
/// </summary>
/// <remarks>Реализация является thread safe.</remarks>
internal sealed class PrepareForViewDocumentFilesService : IPrepareForViewDocumentFilesService
{
  /// <summary>
  /// <inheritdoc />
  /// </summary>
  /// <remarks>
  /// <inheritdoc />
  /// </remarks>
  public event EventHandler<DocumentLocalFileEventArgs> PrepareDocumentFile;

  public void RaisePrepareDocumentFile(
    long objectId,
    int objectTypeId,
    string fileName,
    string filePath)
  {
    EventHandler<DocumentLocalFileEventArgs> prepareDocumentFile = this.PrepareDocumentFile;
    if (prepareDocumentFile == null)
      return;
    prepareDocumentFile((object) null, new DocumentLocalFileEventArgs(objectId, objectTypeId, fileName, filePath));
  }
}
