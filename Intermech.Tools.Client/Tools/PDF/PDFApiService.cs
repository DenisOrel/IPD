// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFApiService
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Data;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFApiService : 
  LibraryApiService,
  IDocumentApiService,
  IExternalApiService,
  IIntegratorService
{
  private readonly LinkedList<OpenPDFFile> openDocumentsList;
  private IApplicationFileTypes fileTypeService;
  private OpenDocumentsApi openDocumentsApi;
  private PDFCodec fileCodec;

  public PDFApiService(IIntegrator owner)
    : base(owner)
  {
    this.openDocumentsList = new LinkedList<OpenPDFFile>();
  }

  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileTypeService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileTypeService = value;
      }
    }
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
    this.fileCodec = new PDFCodec((IServiceProvider) this.Integrator);
    this.openDocumentsApi = new OpenDocumentsApi(this.FileTypeService, (IExternalApiService) this);
    this.openDocumentsApi.OnFindOpenDocument += new Func<string, IOpenDocument>(this.FindOpenDocument);
    this.openDocumentsApi.OnOpenDocument += new Func<string, IOpenDocument>(this.OpenDocument);
    this.openDocumentsApi.OnValidateDocument += new Action<IOpenDocument>(this.ValidateDocument);
    this.openDocumentsApi.OnGetDocumentCodec += new Func<IOpenDocument, IAttributeCodec>(this.GetDocumentCodec);
    this.openDocumentsApi.OnGetDocumentAttributeContainer += new Func<IOpenDocument, IValueBagContainer>(this.GetDocumentAttributeContainer);
    this.openDocumentsApi.OnSaveDocument += new Action<IOpenDocument>(this.SaveDocument);
    this.openDocumentsApi.OnCloseDocument += new Action<IOpenDocument>(this.CloseDocument);
  }

  protected override void DoCloseApiSession(bool topLevelSession)
  {
    if (topLevelSession)
    {
      foreach (OpenPDFFile openDocuments in this.openDocumentsList)
        openDocuments.Dispose();
      this.openDocumentsList.Clear();
    }
    base.DoCloseApiSession(topLevelSession);
  }

  public IOpenDocumentsApi OpenDocuments
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return (IOpenDocumentsApi) this.openDocumentsApi;
    }
  }

  private IOpenDocument FindOpenDocument(string fullPath)
  {
    return (IOpenDocument) CollectionUtils.Find<OpenPDFFile>((IEnumerable<OpenPDFFile>) this.openDocumentsList, (Predicate<OpenPDFFile>) (doc => PathUtils.IsSamePath(doc.FileName, fullPath)));
  }

  private IOpenDocument OpenDocument(string fullPath)
  {
    OpenPDFFile openPdfFile = CollectionUtils.Find<OpenPDFFile>((IEnumerable<OpenPDFFile>) this.openDocumentsList, (Predicate<OpenPDFFile>) (doc => PathUtils.IsSamePath(doc.FileName, fullPath)));
    if (openPdfFile == null)
    {
      openPdfFile = new OpenPDFFile(fullPath);
      this.openDocumentsList.AddFirst(openPdfFile);
    }
    return (IOpenDocument) openPdfFile;
  }

  private void ValidateDocument(IOpenDocument openDocument)
  {
    if (!(openDocument is OpenPDFFile))
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_223"));
  }

  private void SaveDocument(IOpenDocument openDocument)
  {
    ((OpenPDFFile) openDocument).FlushChanges();
  }

  private void CloseDocument(IOpenDocument openDocument)
  {
    OpenPDFFile openPdfFile = (OpenPDFFile) openDocument;
    this.openDocumentsList.Remove(openPdfFile);
    openPdfFile.Dispose();
  }

  private IAttributeCodec GetDocumentCodec(IOpenDocument openDocument)
  {
    return (IAttributeCodec) this.fileCodec;
  }

  private IValueBagContainer GetDocumentAttributeContainer(IOpenDocument openDocument)
  {
    return (IValueBagContainer) openDocument;
  }
}
