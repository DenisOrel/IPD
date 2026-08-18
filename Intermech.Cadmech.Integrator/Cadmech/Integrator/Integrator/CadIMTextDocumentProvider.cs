// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.Integrator.CadIMTextDocumentProvider
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.CADInterface.Proxies.Cadmech;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator.Integrator;

internal class CadIMTextDocumentProvider : IIMTextDocumentProvider
{
  private long documentId;
  private string documentFilePath;
  private CadmechRootProxy cadmechRootProxy;

  public CadIMTextDocumentProvider(
    long documentId,
    string documentFilePath,
    CadmechRootProxy cadmechRootProxy)
  {
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentNullException(nameof (documentFilePath));
    if (cadmechRootProxy == null)
      throw new ArgumentNullException(nameof (cadmechRootProxy));
    this.documentId = documentId;
    this.documentFilePath = documentFilePath;
    this.cadmechRootProxy = cadmechRootProxy;
  }

  public IMTextDocumentProxy GetIMTextDocument(bool throwIfNoCadmechFound)
  {
    return this.cadmechRootProxy.GetDocument(this.documentFilePath);
  }
}
