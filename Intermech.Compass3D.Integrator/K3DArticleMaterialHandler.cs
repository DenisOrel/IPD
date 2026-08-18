// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DArticleMaterialHandler
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Collections;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data.Sync;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DArticleMaterialHandler : CooperativeAction
{
  private MechanicalDriver driver;
  private CaptureChangesDriverContext driverContext;
  private SectionEntity articleItem;
  private SectionEntity materialItem;
  private IArticleMaterialParameterReader materialParameterReader;
  private ICollection<StringKey> synchronizedAttributes;

  public K3DArticleMaterialHandler(
    MechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    SectionEntity articleItem,
    SectionEntity materialItem,
    IArticleMaterialParameterReader materialParameterReader,
    ICollection<StringKey> synchronizedAttributes)
    : base(driverContext.Scheduler)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (driverContext == null)
      throw new ArgumentNullException("ctx");
    if (articleItem == null)
      throw new ArgumentNullException(nameof (driverContext));
    if (materialItem == null)
      throw new ArgumentNullException(nameof (materialItem));
    if (materialParameterReader == null)
      throw new ArgumentNullException(nameof (materialParameterReader));
    if (synchronizedAttributes == null)
      throw new ArgumentNullException(nameof (synchronizedAttributes));
    this.driver = driver;
    this.driverContext = driverContext;
    this.articleItem = articleItem;
    this.materialItem = materialItem;
    this.materialParameterReader = materialParameterReader;
    this.synchronizedAttributes = synchronizedAttributes;
  }

  public ICollection<Tuple<StringKey, StringKey>> AttributeRenameTable { get; set; }

  private CaptureChangesDriverContext DriverContext
  {
    [DebuggerStepThrough] get => this.driverContext;
  }

  private MechanicalDriver Driver
  {
    [DebuggerStepThrough] get => this.driver;
  }

  protected override object GetUIReportOperationId() => (object) this.materialItem;

  protected override IEnumerable<CooperativeState> Coroutine()
  {
    ValueBag valueBag = new ValueBag();
    foreach (StringKey synchronizedAttribute in (IEnumerable<StringKey>) this.synchronizedAttributes)
    {
      ValueRecord parameter = this.materialParameterReader.TryReadParameter(synchronizedAttribute);
      if (parameter != null && !parameter.IsNull)
        valueBag.Add(this.ApplyRenaming(parameter));
    }
    if (valueBag.HasChanges)
      valueBag.AcceptChanges();
    DirectObjectAttributesRef attributableType = new DirectObjectAttributesRef(ObjectSection.GetObjectType(this.materialItem));
    ValueBag table = this.Driver.Operations.Db.ReadObjectAttributes(this.materialItem, (IDBAttributableTypeRef) attributableType);
    AppToDBAttributeSyncTask attributeSyncTask = new AppToDBAttributeSyncTask();
    attributeSyncTask.EntityDisplayName = DisplaySection.GetQualifiedName(this.materialItem);
    attributeSyncTask.EntityId = ObjectSection.GetObjectId(this.materialItem);
    attributeSyncTask.SetApplicationAttributes(valueBag, true);
    attributeSyncTask.SetDatabaseAttributes(table, (IDBAttributableTypeRef) attributableType);
    foreach (StringKey key in (IEnumerable<StringKey>) valueBag.Keys)
      attributeSyncTask.Attributes.Add(new AttributeSyncUnit(key, false));
    attributeSyncTask.RunChecked();
    if (table.HasChanges)
    {
      AttributesSection attributesSection = this.materialItem.Sections.Get<AttributesSection>();
      attributesSection.EmbeddedSet = new ContainerValues(valueBag, true);
      attributesSection.DatabaseSet = table;
      this.Driver.Operations.Db.EmitObjectAttributesServerActions(this.materialItem);
      yield return this.Wait((IWaitObject) this.Driver.SchedulerStages.UIStage);
      this.Driver.Operations.Db.EmitUIActions(this.DriverContext, this.materialItem);
    }
  }

  private ValueRecord ApplyRenaming(ValueRecord parameter)
  {
    if (this.AttributeRenameTable != null)
    {
      Tuple<StringKey, StringKey> tuple = CollectionUtils.Find<Tuple<StringKey, StringKey>>((IEnumerable<Tuple<StringKey, StringKey>>) this.AttributeRenameTable, (Predicate<Tuple<StringKey, StringKey>>) (item => item.Item1 == parameter.Key));
      if (tuple != null)
      {
        ValueRecord valueRecord = new ValueRecord(tuple.Item2, parameter.Value);
        valueRecord.Flags.CopyAll(parameter.Flags);
        return valueRecord;
      }
    }
    return parameter;
  }
}
