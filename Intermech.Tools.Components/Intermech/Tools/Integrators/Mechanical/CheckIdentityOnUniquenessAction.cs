// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.CheckIdentityOnUniquenessAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class CheckIdentityOnUniquenessAction : IAction
{
  private readonly MechanicalDriver driver;
  private readonly CaptureChangesDriverContext ctx;
  private readonly SectionEntity docItem;
  private ObjectSection docObj;
  private AttributesSection docAttrs;

  public CheckIdentityOnUniquenessAction(
    MechanicalDriver driver,
    CaptureChangesDriverContext ctx,
    SectionEntity docItem)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    this.driver = driver;
    this.ctx = ctx;
    this.docItem = docItem;
    this.docObj = docItem.Sections.Get<ObjectSection>();
    this.docAttrs = docItem.Sections.Get<AttributesSection>();
  }

  private MechanicalDriver MechanicalDriver => this.driver;

  public void Perform()
  {
    ValueRecord identityAttribute = DbOperations.FindIdentityAttribute(this.docItem, (IEnumerable<StringKey>) this.MechanicalDriver.Operations.Documents.GetIdentityKeys(), false);
    if (identityAttribute == null || !FileVars.SoftMode.Value)
      return;
    SameDocSection sameDoc = this.SameDocExists(identityAttribute);
    if (sameDoc == null)
      return;
    this.RepairIdentity(identityAttribute, sameDoc);
    if (!UIReport.Enabled)
      return;
    UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Components_515"), (object) DisplaySection.GetDisplayName(this.docItem), (object) sameDoc.IdentityValue), TraceLevel.Warning);
  }

  private SameDocSection SameDocExists(ValueRecord docId)
  {
    SectionEntity sectionEntity1 = this.ctx.Database.QueryFirst((IQueryCondition) new BinaryCondition((object) SameDocSection.IdentityValueRef, BinaryOperator.Equal, (object) docId.Read<string>(string.Empty)));
    if (sectionEntity1 != null)
      return sectionEntity1.Sections.Get<SameDocSection>();
    SameDocSection sectionObject = this.SameDocInWorkContext(docId) ?? this.SameDocInBase(docId);
    if (sectionObject == null)
      return (SameDocSection) null;
    SectionEntity sectionEntity2 = new SectionEntity();
    sectionEntity2.Sections.Set((object) sectionObject);
    this.ctx.Database.Add((IEntity) sectionEntity2);
    this.MechanicalDriver.SchedulerStages.DiskWritesStage.Wait((IAction) new MarkAsRequireIdentityCheckAction(this.ctx, sectionEntity2));
    return sectionObject;
  }

  private SameDocSection SameDocInWorkContext(ValueRecord docId)
  {
    List<StringKey> keyOnly = new List<StringKey>();
    keyOnly.Add(docId.Key);
    SectionEntity sectionEntity = this.ctx.Database.QueryFirst((IQueryCondition) new CodeCondition((Predicate<IEntity>) (dbItem =>
    {
      SectionEntity objectItem = (SectionEntity) dbItem;
      if (objectItem != this.docItem && objectItem.Sections.Contains<ObjectSection>() && objectItem.Sections.Contains<FilesSection>() && objectItem.Sections.Contains<AttributesSection>())
      {
        ValueRecord identityAttribute = DbOperations.FindIdentityAttribute(objectItem, (IEnumerable<StringKey>) keyOnly, false);
        if (identityAttribute != null && object.Equals(identityAttribute.Value, docId.Value))
          return true;
      }
      return false;
    })));
    if (sectionEntity == null)
      return (SameDocSection) null;
    IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    string relativePath = PathUtils.GetRelativePath(FilesSection.GetMasterFile(sectionEntity), service.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
    return new SameDocSection(new SameDocReference(sectionEntity), relativePath, docId.Read<string>(string.Empty));
  }

  private SameDocSection SameDocInBase(ValueRecord docId)
  {
    ConditionStructure conditionStructure = new ConditionStructure((string) docId.Key, RelationalOperators.Equal, docId.Value, LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = sessionKeeper.Session.GetObjectCollection(IDCache.Default.AllDocuments.Id).Select(paramSet);
    if (dataTable.Rows.Count > 0)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
      if (int64 != this.docObj.ObjectId)
      {
        string masterFileName = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true).DBFilesInfo.GetMasterFileName(int64, true);
        return new SameDocSection(new SameDocReference(int64), masterFileName, docId.Read<string>(string.Empty));
      }
    }
    return (SameDocSection) null;
  }

  private void RepairIdentity(ValueRecord docId, SameDocSection sameDoc)
  {
    int num = docId.Key == (StringKey) IDCache.Default.Designation.Text ? 1 : 0;
    string origDesignation = sameDoc.IdentityValue;
    if (num != 0)
      origDesignation = DocumentDesignationHelper.RemoveDocCode(origDesignation, this.docObj.ObjectType);
    string str = $"{origDesignation} [{Stopwatch.GetTimestamp()}]";
    if (num != 0)
      str = DocumentDesignationHelper.AppendDocCode(str, this.docObj.ObjectType);
    this.docAttrs.EmbeddedSet.Bag.Update((StringKey) CADDocumentResources.EMB_IndependentDesignation, (object) "1");
    this.docAttrs.EmbeddedSet.Bag.CopyFlag((StringKey) CADDocumentResources.EMB_IndependentDesignation, docId.Flags, NamedFlags.ThrowSetException);
    this.docAttrs.WorkingSet.Update(docId.Key, (object) str);
    this.docAttrs.WorkingSet.Update((StringKey) CADDocumentResources.EMB_IndependentDesignation, (object) "1");
    this.docAttrs.WorkingSet.CopyFlag((StringKey) CADDocumentResources.EMB_IndependentDesignation, docId.Flags, NamedFlags.ThrowSetException);
    this.docAttrs.DatabaseSet.Update((StringKey) IDCache.Default.RequireIdentityCheck.Text, (object) sameDoc.IdentityValue);
    this.docAttrs.DatabaseSet.CopyFlag((StringKey) IDCache.Default.RequireIdentityCheck.Text, docId.Flags, NamedFlags.ThrowSetException);
  }
}
