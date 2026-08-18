// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.JTDerivedDocumentHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.ControlFlow.Cooperative;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class JTDerivedDocumentHandler(
  DocumentCaptureChangesDriver driver,
  CaptureChangesDriverContext ctx,
  SectionEntity docItem) : DocumentHandlerBase(driver, ctx, docItem)
{
  private JTDerivedFileInfo jtDerivedFileInfo;

  protected override void InitializeHandler()
  {
    base.InitializeHandler();
    this.BindToJTDocument();
  }

  private void BindToJTDocument()
  {
    this.jtDerivedFileInfo = this.DocumentEntity.Sections.Get<JTDerivedFileInfo>((JTDerivedFileInfo) null);
    if (this.jtDerivedFileInfo == null)
    {
      this.jtDerivedFileInfo = new JTDerivedFileInfo(this.DocumentFiles.MasterFile);
      this.jtDerivedFileInfo.Refresh();
      this.DocumentEntity.Sections.Set((object) this.jtDerivedFileInfo);
    }
    if (!this.jtDerivedFileInfo.IsDerivedFromJTFile)
      throw new FaultException($"Документ '{DisplaySection.GetDisplayName(this.DocumentEntity)}' должен быть компонентом, основанном на JT-представлении другого документа.");
    if (this.jtDerivedFileInfo.JTDocumentId == 0L || DBHelper.GetObjectType(this.jtDerivedFileInfo.JTDocumentId) != IDCache.Default.JTDocuments.Id)
      throw new FaultException($"Документ '{DisplaySection.GetDisplayName(this.DocumentEntity)}' не может быть обработан, так как используемое им JT-представление '{this.jtDerivedFileInfo.JTFilePath}' не является документом типа '{IDCache.Default.JTDocuments.Text}'.");
  }

  protected override void ProcessDependencies()
  {
    new JTDerivedDocumentDependenciesBuilder(this.Driver, this.DriverContext, (IDocumentBuilder) this.Driver, this.jtDerivedFileInfo.JTFilePath).Run(this.DocumentEntity);
  }

  protected override ContainerValues ReadFileProperties()
  {
    return new ContainerValues(AlternativeRepresentationsHelper.CopyAttributes(this.jtDerivedFileInfo.JTDocumentId, IDCache.Default.JTDocuments.Id), false);
  }

  protected override bool WriteFileProperties(ContainerValues fileProperties)
  {
    throw new NotSupportedException();
  }

  protected override ValueBag DecodeDocumentAttributes(ContainerValues fileProperties)
  {
    ValueBag valueBag = fileProperties.Bag.Copy();
    ValueRecord valueRecord = valueBag.Find((StringKey) IDCache.Default.Designation.Text);
    if (valueRecord != null)
      valueRecord.Value = (object) DocumentDesignationHelper.AppendDocCode((string) valueRecord.Value, this.DocumentObject.ObjectType);
    valueBag.AcceptChanges();
    return valueBag;
  }

  protected override void EncodeDocumentAttributes(
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
  }

  protected override ICollection<StringKey> GetTransferableAttributes()
  {
    ICollection<StringKey> transferableAttributes = base.GetTransferableAttributes();
    transferableAttributes.AddRange<StringKey>((IEnumerable<StringKey>) this.DocumentAttributes.WorkingSet.Keys);
    return transferableAttributes;
  }

  protected override bool IsTransferRequired(StringKey attributeKey)
  {
    return attributeKey == (StringKey) IDCache.Default.Designation.Text || attributeKey == (StringKey) IDCache.Default.Name.Text;
  }

  /// <summary>
  /// Сохраняет измененные файлы документа на диск. Этот метод используется для сохранения на диск любых изменений в файле документа, а не только
  /// сделанных интегратором. Например, метод будет вызван, если пользователь не сохранил документ в приложении-редакторе перед вызовом команды IPS.
  /// </summary>
  protected override IEnumerable<CooperativeState> SaveModifiedDocumentFiles()
  {
    yield break;
  }

  protected override void ProcessRelations()
  {
    new SyncDocumentStructureAction(this.Driver, this.DriverContext, this.DocumentEntity)
    {
      UseFixedRelations = true
    }.Perform();
  }
}
