// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SidecarObjectsGeneratorAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class SidecarObjectsGeneratorAction : CooperativeAction
{
  private MechanicalDriver driver;
  private List<SectionEntity> sourceDocuments;

  private SidecarObjectsGeneratorAction(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext)
    : base(driverContext.Scheduler)
  {
    this.driver = driver != null ? driver : throw new ArgumentNullException(nameof (driver));
    this.sourceDocuments = new List<SectionEntity>();
  }

  public static SidecarObjectsGeneratorAction GetOrCreate(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    return CaptureChangesDatabaseGlobals<SidecarObjectsGeneratorAction>.GetOrCreate(driverContext.Database, (Func<SidecarObjectsGeneratorAction>) (() =>
    {
      SidecarObjectsGeneratorAction task = new SidecarObjectsGeneratorAction(driver, driverContext);
      driverContext.Scheduler.AddTask((IAction) task);
      return task;
    }));
  }

  public void AddSourceDocument(SectionEntity documentEntity)
  {
    if (documentEntity == null)
      throw new ArgumentNullException(nameof (documentEntity));
    this.sourceDocuments.Add(documentEntity);
  }

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    yield return this.Wait((IWaitObject) this.driver.SchedulerStages.DerivedObjectsStage);
    foreach (ISidecarObjectsCaptureChangesExtension objectsExtension in (IEnumerable<ISidecarObjectsCaptureChangesExtension>) this.driver.SidecarObjectsExtensions)
      this.GenerateSidecarObjects(objectsExtension);
  }

  private void GenerateSidecarObjects(ISidecarObjectsCaptureChangesExtension @extension)
  {
    List<SectionEntity> all = this.sourceDocuments.FindAll(new Predicate<SectionEntity>(@extension.IsSourceDocument));
    if (all.Count == 0)
      return;
    ICollection<Tuple<SectionEntity, long>> existing = @extension.FindExisting((IList<SectionEntity>) all);
    foreach (Tuple<SectionEntity, long> tuple in (IEnumerable<Tuple<SectionEntity, long>>) existing)
    {
      SectionEntity documentEntity = tuple.Item1;
      long sidecarObjectId = tuple.Item2;
      @extension.Update(documentEntity, sidecarObjectId);
    }
    foreach (SectionEntity sectionEntity in all)
    {
      SectionEntity sourceDocument = sectionEntity;
      if (!CollectionUtils.Exists<Tuple<SectionEntity, long>>((IEnumerable<Tuple<SectionEntity, long>>) existing, (Predicate<Tuple<SectionEntity, long>>) (x => x.Item1 == sourceDocument)) && @extension.CanCreate(sourceDocument))
        @extension.Create(sourceDocument);
    }
  }
}
