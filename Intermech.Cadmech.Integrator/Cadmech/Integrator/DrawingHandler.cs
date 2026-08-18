// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DrawingHandler
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DrawingHandler(
  ConstructionalExtension driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : DocumentHandlerBase((DocumentCaptureChangesDriver) driver, ctx, docItem)
{
  private ConstructionalExtension Driver
  {
    [DebuggerStepThrough] get => (ConstructionalExtension) base.Driver;
  }

  protected override void ProcessDependencies()
  {
    new ConstructionalDependenciesBuilder(this.Driver, this.DriverContext).Run(this.DocumentEntity);
  }

  protected override ContainerValues ReadFileProperties()
  {
    return DwgOperations.GetStamp(this.Driver.Integrator, this.DocumentEntity, this.Driver.DrawingTypes.GetSettings(this.DocumentObject.ObjectType));
  }

  protected override bool WriteFileProperties(ContainerValues fileProperties)
  {
    throw new NotSupportedException($"Интегратор не имеет возможности записать измененные атрибуты в файл чертежа '{this.DocumentFiles.MasterFile}'.");
  }

  protected override ValueBag DecodeDocumentAttributes(ContainerValues fileProperties)
  {
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    ValueBag attributes = new ValueBag();
    foreach (ValueRecord valueRecord in fileProperties.Bag)
      this.EmitDecodeAction(valueRecord.Key, fileProperties, attributes, this.DocumentObject.ObjectType).Perform();
    attributes.AcceptChanges();
    return attributes;
  }

  private IAction EmitDecodeAction(
    StringKey attributeKey,
    ContainerValues fileProperties,
    ValueBag attributes,
    int docType)
  {
    if (attributeKey == (StringKey) IDCache.Default.Designation.Text)
      return (IAction) new DecodeDocumentDesignationAction(fileProperties.Bag, attributeKey, attributes, attributeKey, docType);
    return attributeKey == (StringKey) IDCache.Default.Name.Text ? (IAction) new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey), typeof (string), true) : (IAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey);
  }

  protected override void EncodeDocumentAttributes(
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
  }

  protected override void CorrectAttributes()
  {
    base.CorrectAttributes();
    new FillEmptyDocumentIdentityHandler((DocumentCaptureChangesDriver) this.Driver, this.DocumentEntity).Perform();
  }

  protected override ICollection<StringKey> GetTransferableAttributes()
  {
    ICollection<StringKey> transferableAttributes = base.GetTransferableAttributes();
    transferableAttributes.AddRange<StringKey>((IEnumerable<StringKey>) this.DocumentAttributes.WorkingSet.Keys);
    return transferableAttributes;
  }

  protected override PathCollection CollectNewAncillaryFiles()
  {
    PathCollection pathCollection = base.CollectNewAncillaryFiles();
    if (this.Driver.DrawingTypes.GetSettings(this.DocumentObject.ObjectType).XRefMode == XRefMode.AncillaryFiles)
    {
      List<string> liveXrefs = DwgOperations.GetLiveXRefs(this.Driver.Integrator, this.DocumentFiles.MasterFile);
      DwgOperations.FilterLiveXRefs(this.DocumentFiles.MasterFile, liveXrefs);
      foreach (string str in liveXrefs)
        pathCollection.Add(str);
    }
    return pathCollection;
  }

  protected override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    CadApiService service = ServiceUtils.GetService<CadApiService>((object) this.Driver.Integrator, true);
    if (service.IsApplicationRunning)
    {
      using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) service))
      {
        ICadDocumentProxy openDocument = acadApiSession.Application.FindOpenDocument(this.DocumentFiles.MasterFile);
        if (openDocument != null && openDocument.Modified && !openDocument.IsReadOnly)
        {
          openDocument.Save();
          yield break;
        }
      }
    }
  }

  protected override void ProcessRelations()
  {
    base.ProcessRelations();
    if (this.Driver.DrawingTypes.GetSettings(this.DocumentObject.ObjectType).XRefMode != XRefMode.Documents)
      return;
    new SyncDocumentStructureAction((DocumentCaptureChangesDriver) this.Driver, this.DriverContext, this.DocumentEntity).Perform();
  }
}
