// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentCaptureChangesOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

public class DocumentCaptureChangesOperations
{
  private CaptureChangesDriverContext driverContext;
  private DbOperations dbObjects;
  private CheckoutOperations checkout;
  private DocumentOperations documents;
  private DraftDocumentOperations draftDocuments;

  public DocumentCaptureChangesOperations(
    CaptureChangesDriverContext driverContext,
    IDraftDocumentsService draftDocumentsService)
  {
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    if (draftDocumentsService == null)
      throw new ArgumentNullException(nameof (draftDocumentsService));
    this.driverContext = driverContext;
    this.checkout = new CheckoutOperations();
    this.dbObjects = new DbOperations(this.checkout);
    this.documents = new DocumentOperations();
    this.draftDocuments = new DraftDocumentOperations(driverContext, draftDocumentsService);
  }

  public CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }

  public DbOperations Db
  {
    [DebuggerStepThrough] get => this.dbObjects;
  }

  public DocumentOperations Documents
  {
    [DebuggerStepThrough] get => this.documents;
  }

  public CheckoutOperations Checkout
  {
    [DebuggerStepThrough] get => this.checkout;
  }

  public DraftDocumentOperations DraftDocuments
  {
    [DebuggerStepThrough] get => this.draftDocuments;
  }
}
