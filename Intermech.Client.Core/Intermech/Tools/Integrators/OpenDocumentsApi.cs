
// Type: Intermech.Tools.Integrators.OpenDocumentsApi
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Data;
using System;


namespace Intermech.Tools.Integrators;

public sealed class OpenDocumentsApi : IOpenDocumentsApi
{
  private readonly IApplicationFileTypes fileTypesSvc;
  private readonly IExternalApiService apiSvc;

  public OpenDocumentsApi(IApplicationFileTypes fileTypesService, IExternalApiService apiService)
  {
    if (fileTypesService == null)
      throw new ArgumentNullException(nameof (fileTypesService));
    if (apiService == null)
      throw new ArgumentNullException(nameof (apiService));
    this.fileTypesSvc = fileTypesService;
    this.apiSvc = apiService;
  }

  public IOpenDocument FindOpenDocument(string fullPath)
  {
    if (fullPath == null)
      throw new ArgumentNullException(nameof (fullPath));
    if (!this.fileTypesSvc.IsApplicationFile(fullPath))
      throw new InvalidOperationException($"The file '{fullPath}' is not supported by integrator");
    if (this.OnFindOpenDocument == null)
      throw new InvalidOperationException("No event handler assigned to find a open document.");
    this.apiSvc.CheckApiSessionOpen();
    return this.OnFindOpenDocument(fullPath);
  }

  public IOpenDocument OpenDocument(string fullPath)
  {
    if (fullPath == null)
      throw new ArgumentNullException(nameof (fullPath));
    if (!this.fileTypesSvc.IsApplicationFile(fullPath))
      throw new InvalidOperationException($"The file '{fullPath}' is not supported by integrator");
    if (this.OnOpenDocument == null)
      throw new InvalidOperationException("No event handler assigned to open a document.");
    this.apiSvc.CheckApiSessionOpen();
    return this.OnOpenDocument(fullPath);
  }

  public IAttributeCodec GetCodec(IOpenDocument openDocument)
  {
    if (openDocument == null)
      throw new ArgumentNullException(nameof (openDocument));
    this.ValidateDocument(openDocument);
    if (this.OnGetDocumentCodec == null)
      throw new InvalidOperationException("No event handler assigned to get a document codec.");
    this.apiSvc.CheckApiSessionOpen();
    return this.OnGetDocumentCodec(openDocument);
  }

  public IValueBagContainer GetAttributeContainer(IOpenDocument openDocument)
  {
    if (openDocument == null)
      throw new ArgumentNullException(nameof (openDocument));
    this.ValidateDocument(openDocument);
    if (this.OnGetDocumentAttributeContainer == null)
      throw new InvalidOperationException("No event handler assigned to get a document attribute container.");
    this.apiSvc.CheckApiSessionOpen();
    return this.OnGetDocumentAttributeContainer(openDocument);
  }

  public void Save(IOpenDocument openDocument)
  {
    if (openDocument == null)
      throw new ArgumentNullException(nameof (openDocument));
    this.ValidateDocument(openDocument);
    this.apiSvc.CheckApiSessionOpen();
    if (this.OnSaveDocument == null)
      return;
    this.OnSaveDocument(openDocument);
  }

  public void Close(IOpenDocument openDocument)
  {
    if (openDocument == null)
      throw new ArgumentNullException(nameof (openDocument));
    this.ValidateDocument(openDocument);
    this.apiSvc.CheckApiSessionOpen();
    if (this.OnCloseDocument == null)
      return;
    this.OnCloseDocument(openDocument);
  }

  private void ValidateDocument(IOpenDocument openDocument)
  {
    if (this.OnValidateDocument == null)
      return;
    this.OnValidateDocument(openDocument);
  }

  public event Func<string, IOpenDocument> OnFindOpenDocument;

  public event Func<string, IOpenDocument> OnOpenDocument;

  public event Action<IOpenDocument> OnValidateDocument;

  public event Func<IOpenDocument, IAttributeCodec> OnGetDocumentCodec;

  public event Func<IOpenDocument, IValueBagContainer> OnGetDocumentAttributeContainer;

  public event Action<IOpenDocument> OnSaveDocument;

  public event Action<IOpenDocument> OnCloseDocument;
}
