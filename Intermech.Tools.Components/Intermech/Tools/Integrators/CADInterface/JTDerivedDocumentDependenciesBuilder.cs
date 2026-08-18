// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.JTDerivedDocumentDependenciesBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class JTDerivedDocumentDependenciesBuilder : FileDependenciesHandler
{
  private readonly string jtFilePath;

  public JTDerivedDocumentDependenciesBuilder(
    DocumentCaptureChangesDriver driver,
    CaptureChangesDriverContext ctx,
    IDocumentBuilder documentBuilder,
    string jtFilePath)
    : base(ctx, documentBuilder, ClientContext.FileVault, driver.Operations.DraftDocuments)
  {
    this.jtFilePath = jtFilePath != null ? jtFilePath : throw new ArgumentNullException(nameof (jtFilePath));
  }

  protected override void CollectDependencies()
  {
    base.CollectDependencies();
    this.DocumentDependencies.Add(new DocumentFileData(this.jtFilePath, true));
  }

  protected override FileDependencyProcessingParameters GetDependencyProcessingParameters(
    FileDependencyProcessingData dependency)
  {
    return FileDependencyProcessingParameters.Ignore;
  }
}
