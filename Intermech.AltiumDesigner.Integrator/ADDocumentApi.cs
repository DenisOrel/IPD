// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADDocumentApi
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal abstract class ADDocumentApi : IDocumentCADApiService
{
  protected readonly ADMechanicalDriver driver;
  protected readonly CaptureChangesDriverContext driverContext;
  protected readonly SettingsService settingsSvc;
  protected readonly ADInterfaceService apiSvc;

  public ADDocumentApi(ADMechanicalDriver driver, CaptureChangesDriverContext driverContext)
  {
    this.driver = driver ?? throw new ArgumentNullException(nameof (driver));
    this.driverContext = driverContext ?? throw new ArgumentNullException(nameof (driverContext));
    this.settingsSvc = ServiceUtils.GetService<SettingsService>((object) driver.Integrator, true);
    this.apiSvc = ServiceUtils.GetService<ADInterfaceService>((object) driver.Integrator, true);
  }

  public IFileDependenciesHandler TryGetFileDependenciesHandler(SectionEntity docItem)
  {
    AddInProxy proxy = docItem != null ? docItem.Sections.Get<AddInProxy>() : throw new ArgumentNullException(nameof (docItem));
    FilesSection file = docItem.Sections.Get<FilesSection>();
    return this.OnTryGetFileDependenciesHandler(docItem, proxy, file);
  }

  public string GetDocumentTypeAttributeName(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    return "Document type";
  }

  public abstract List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem);

  public virtual ContainerValues ReadDocumentProperties(SectionEntity docItem)
  {
    return docItem != null ? this.documentAttributeCodec.ReadFileProperties(this.GetBagContainer(docItem), this.GetDocumentFileAttributes(docItem)) : throw new ArgumentNullException(nameof (docItem));
  }

  public virtual bool WriteDocumentProperties(SectionEntity docItem, ContainerValues fileProperties)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    return this.documentAttributeCodec.Formatter.Write(this.GetBagContainer(docItem), fileProperties);
  }

  public void SaveDocumentFile(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    docItem.Sections.Get<AddInProxy>().AddIn.SaveObject(docItem.Sections.Get<FilesSection>().MasterFile);
  }

  public virtual ValueBag DecodeDocumentAttributes(
    SectionEntity docItem,
    ContainerValues fileProperties)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    DecodeAttributesOptions decodeOptions = this.driver.Operations.Documents.GetDecodeOptions(docItem);
    return this.documentAttributeCodec.Decode(new DecodeAttributesParams(this.GetBagContainer(docItem), this.GetDocumentFileAttributes(docItem), fileProperties, decodeOptions));
  }

  public virtual void EncodeDocumentAttributes(
    SectionEntity docItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    if (attributeKeys == null)
      throw new ArgumentNullException(nameof (attributeKeys));
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    EncodeAttributesOptions encodeOptions = this.driver.Operations.Documents.GetEncodeOptions(docItem);
    this.documentAttributeCodec.Encode(new EncodeAttributesParams(this.GetBagContainer(docItem), attributeKeys, attributes, fileProperties, encodeOptions)
    {
      ContainerDisplayName = DisplaySection.GetQualifiedName(docItem)
    });
  }

  public virtual void ProcessDocumentAttributes(
    SectionEntity documentItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    if (workingSet == null)
      throw new ArgumentNullException(nameof (workingSet));
    if (databaseSet == null)
      throw new ArgumentNullException(nameof (databaseSet));
  }

  public ICollection<StringKey> GetDocumentSyncAttributes(SectionEntity docItem)
  {
    return this.documentAttributes.GetAttributes(ObjectSection.TryGetObjectType(docItem), false);
  }

  public List<string> GetSatelliteFiles(SectionEntity docItem)
  {
    AddInProxy proxy = docItem != null ? docItem.Sections.Get<AddInProxy>() : throw new ArgumentNullException(nameof (docItem));
    FilesSection file = docItem.Sections.Get<FilesSection>();
    return this.OnGetSatelliteFiles(docItem, proxy, file);
  }

  public virtual List<string> GetPrivateFiles(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    return new List<string>(0);
  }

  public virtual ICollection<InitialArticleData> ReadArticles(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    return (ICollection<InitialArticleData>) new List<InitialArticleData>(0);
  }

  public virtual ValueBag TryReadDocumentRelationAttributes(
    SectionEntity projectDocument,
    SectionEntity partDocument)
  {
    return (ValueBag) null;
  }

  protected ICollection<StringKey> GetDocumentFileAttributes(SectionEntity docItem)
  {
    return this.documentAttributes.GetAttributes(ObjectSection.TryGetObjectType(docItem), false);
  }

  protected abstract List<string> OnGetSatelliteFiles(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file);

  protected abstract IValueBagContainer GetBagContainer(SectionEntity docItem);

  protected abstract IFileDependenciesHandler OnTryGetFileDependenciesHandler(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file);

  protected abstract IAttributeCodec documentAttributeCodec { get; }

  protected abstract ISynchronizedObjectAttributes documentAttributes { get; }
}
