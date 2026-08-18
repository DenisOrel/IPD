// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CreateIMViewerBlankObjectAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Services.IMViewer;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class CreateIMViewerBlankObjectAction : IAction
{
  private SectionEntity sourceDocumentEntity;
  private SectionEntity imviewerObjectEntity;
  private IIMViewerObjectCreatorService imviewerService;

  public CreateIMViewerBlankObjectAction(
    SectionEntity sourceDocumentEntity,
    SectionEntity imviewerObjectEntity,
    IIMViewerObjectCreatorService imviewerService)
  {
    if (sourceDocumentEntity == null)
      throw new ArgumentException(nameof (sourceDocumentEntity));
    if (imviewerObjectEntity == null)
      throw new ArgumentException(nameof (imviewerObjectEntity));
    if (imviewerService == null)
      throw new ArgumentNullException(nameof (imviewerService));
    this.sourceDocumentEntity = sourceDocumentEntity;
    this.imviewerObjectEntity = imviewerObjectEntity;
    this.imviewerService = imviewerService;
  }

  public void Perform()
  {
    ObjectSection objectSection = this.sourceDocumentEntity.Sections.Get<ObjectSection>();
    new DBObjectEntityRef(this.imviewerObjectEntity).UpdateObjectId(this.imviewerService.CreateEmptyViewerObject(objectSection.ObjectId, objectSection.ObjectType, true));
  }

  public override string ToString() => "Создание в базе IPS заготовки для нового объекта IMViewer";
}
