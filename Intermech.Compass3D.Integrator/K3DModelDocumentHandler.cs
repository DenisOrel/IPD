// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DModelDocumentHandler
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Collections;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DModelDocumentHandler : ModelHandler
{
  private static readonly Lazy<StringKey[]> ancillaryDrawingAttributeNames = new Lazy<StringKey[]>(new Func<StringKey[]>(K3DModelDocumentHandler.CreateAncillaryDrawingAttributeNames), LazyThreadSafetyMode.PublicationOnly);
  private K3DCaptureChangesDriver k3dDriver;
  private IModelDrawingsService modelDrawingsService;
  private K3DAncillaryDrawingsService ancillaryDrawingsService;
  private string ancillaryDrawingFileName;
  private CADDocumentProxy ancillaryDrawing;
  private ContainerValues acillaryDrawingFileProperties;
  private ValueBag ancillaryDrawingAttributes;

  public K3DModelDocumentHandler(
    K3DCaptureChangesDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity modelItem)
    : base((MechanicalDriver) driver, ctx, modelItem)
  {
    this.k3dDriver = driver;
    this.modelDrawingsService = ServiceUtils.GetService<IModelDrawingsService>((object) this.k3dDriver.Integrator, true);
    this.ancillaryDrawingsService = ServiceUtils.GetService<K3DAncillaryDrawingsService>((object) this.k3dDriver.Integrator, true);
  }

  private K3DCaptureChangesDriver K3DDriver
  {
    [DebuggerStepThrough] get => this.k3dDriver;
  }

  protected override void CorrectAttributes()
  {
    this.TryOpenAncillaryDrawing();
    if (this.IsAncillaryDrawingPresent())
      this.UpdateAttributesFromAncillaryDrawing();
    base.CorrectAttributes();
  }

  protected override void WriteChangesToDocumentFiles()
  {
    base.WriteChangesToDocumentFiles();
    if (!this.IsAncillaryDrawingPresent())
      return;
    this.WriteFilePropertiesToAncillaryDrawing(this.DocumentAttributes.EmbeddedSet);
  }

  protected override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    yield return this.Call((Func<IEnumerable<CooperativeState>>) ([DebuggerHidden] () => base.SaveModifiedDocumentFiles()));
    if (this.IsAncillaryDrawingPresent())
      this.SaveAncillaryFileDrawing();
  }

  private void TryOpenAncillaryDrawing()
  {
    if (!this.ancillaryDrawingsService.IsProcessingEnabled)
      return;
    this.ancillaryDrawingFileName = this.FindFirstAncillaryDrawingFile();
    if (string.IsNullOrEmpty(this.ancillaryDrawingFileName))
      return;
    this.ancillaryDrawing = this.K3DDriver.CADSystem.OpenDocument(this.ancillaryDrawingFileName, false);
  }

  private bool IsAncillaryDrawingPresent()
  {
    return !string.IsNullOrEmpty(this.ancillaryDrawingFileName) && this.ancillaryDrawing != null;
  }

  private string FindFirstAncillaryDrawingFile()
  {
    if (this.DocumentFiles.Satellites.Count != 0)
    {
      string ancillaryDrawingFile = CollectionUtils.Find<string>((IEnumerable<string>) this.DocumentFiles.Satellites, new Predicate<string>(this.modelDrawingsService.IsDrawingFileName));
      if (!string.IsNullOrEmpty(ancillaryDrawingFile))
        return ancillaryDrawingFile;
    }
    return (string) null;
  }

  private void UpdateAttributesFromAncillaryDrawing()
  {
    this.acillaryDrawingFileProperties = this.K3DDriver.ApiService.GetDocumentCodec(this.ancillaryDrawing).ReadFileProperties(this.K3DDriver.ApiService.GetDocumentAttributeContainer(this.ancillaryDrawing), (ICollection<StringKey>) this.GetAncillaryDrawingAttributeNames());
    this.ancillaryDrawingAttributes = this.DecodeDocumentAttributes(this.acillaryDrawingFileProperties);
    foreach (ValueRecord drawingAttribute in this.ancillaryDrawingAttributes)
      this.DocumentAttributes.WorkingSet.TryUpdate(drawingAttribute.Key, drawingAttribute.ReadValueOrTypedNull(), true);
  }

  private StringKey[] GetAncillaryDrawingAttributeNames()
  {
    return K3DModelDocumentHandler.ancillaryDrawingAttributeNames.Value;
  }

  private static StringKey[] CreateAncillaryDrawingAttributeNames()
  {
    return new StringKey[4]
    {
      (StringKey) IDCache.Default.Format.Text,
      (StringKey) IDCache.Default.Scale.Text,
      (StringKey) IDCache.Default.NumberOfSheets.Text,
      (StringKey) IDCache.Default.LetterOfSheet.Text
    };
  }

  private void WriteFilePropertiesToAncillaryDrawing(ContainerValues modelFileProperties)
  {
    if (this.acillaryDrawingFileProperties == null)
      return;
    ContainerValues values = this.acillaryDrawingFileProperties.Clone();
    foreach (ValueRecord valueRecord in modelFileProperties.Bag)
    {
      if (!valueRecord.IsNull || values.Bag.Exists(valueRecord.Key))
      {
        StringKey key = valueRecord.Key;
        object newValue = valueRecord.IsNull ? (object) string.Empty : valueRecord.Value;
        values.Bag.TryUpdate(key, newValue, values.IsOpenMetadata);
      }
    }
    if (!values.Bag.HasChanges)
      return;
    this.K3DDriver.ApiService.GetDocumentCodec(this.ancillaryDrawing).Formatter.Write(this.K3DDriver.ApiService.GetDocumentAttributeContainer(this.ancillaryDrawing), values);
    AnalyzerChangesSection.Mark(this.DocumentEntity);
  }

  private void SaveAncillaryFileDrawing()
  {
    if (!this.ancillaryDrawing.Modified || this.ancillaryDrawing.ReadOnly)
      return;
    this.ancillaryDrawing.Save();
  }
}
