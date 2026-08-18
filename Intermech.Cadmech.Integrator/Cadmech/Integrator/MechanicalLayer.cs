// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalLayer
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Cadmech.Integrator.DwgTasks;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MechanicalLayer : IArticleCADApiService, IDocumentCADApiService
{
  private const string NoAttributeWriteSupport = "Интегратор не имеет возможности записать измененные атрибуты в файл чертежа.";
  private const string NoModelSaveSupport = "Интегратор не имеет возможности сохранить изменения в файле чертежа.";
  private readonly MechanicalDwgDriver driver;
  private readonly CaptureChangesDriverContext ctx;
  private readonly IApplicationFileTypes fileTypeService;

  public MechanicalLayer(MechanicalDwgDriver driver, CaptureChangesDriverContext driverContext)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (driverContext == null)
      throw new ArgumentNullException(nameof (driverContext));
    this.driver = driver;
    this.ctx = driverContext;
    this.fileTypeService = ServiceUtils.GetService<IApplicationFileTypes>((object) driver.Integrator, true);
  }

  public IFileDependenciesHandler TryGetFileDependenciesHandler(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    return (IFileDependenciesHandler) new MechanicalDwgDependenciesBuilder(this.driver, this.ctx);
  }

  public List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    if (!this.fileTypeService.IsApplicationFile(FilesSection.GetMasterFile(docItem)))
      throw new NotSupportedException($"Файл '{FilesSection.GetMasterFile(docItem)}' не является документом приложения.");
    if (this.driver.DriverDatabase.IsEntryPointDocument(docItem) && this.driver.RootDocumentGroup != Guid.Empty)
    {
      if (!this.driver.DrawingTypes.IsGroupSupported(this.driver.RootDocumentGroup))
        throw new InvalidOperationException();
      return this.driver.GetTypesByMechanicalDocumentKind(this.driver.RootDocumentGroup == MechanicalSettings.AssemblyDrawingsGroup ? MechanicalDocumentKind.AssemblyModel : MechanicalDocumentKind.PartModel);
    }
    List<MechanicalDocumentKind> list = CollectionUtils.CreateList<MechanicalDocumentKind>(MechanicalDocumentKind.AssemblyModel, MechanicalDocumentKind.PartModel);
    List<LocalId<int>> possibleTypes = new List<LocalId<int>>();
    foreach (MechanicalDocumentKind documentKind in list)
      possibleTypes.AddRange((IEnumerable<LocalId<int>>) this.driver.GetTypesByMechanicalDocumentKind(documentKind));
    return this.DetectNewDrawingType(docItem, possibleTypes) ?? possibleTypes;
  }

  private List<LocalId<int>> DetectNewDrawingType(
    SectionEntity docItem,
    List<LocalId<int>> possibleTypes)
  {
    List<StringKey> attributes = new List<StringKey>();
    attributes.Add((StringKey) Intermech.Localization.Localization.rm.GetString("EMB_DesignType"));
    attributes.Add((StringKey) IDCache.Default.Designation.Text);
    attributes.Add((StringKey) IDCache.Default.Name.Text);
    PathDictionary<ValueBag> pathDictionary = new PathDictionary<ValueBag>();
    foreach (LocalId<int> possibleType in possibleTypes)
    {
      if (!(this.driver.DrawingTypes.GetGroupTypeByDrawingType(possibleType.Id, true) != MechanicalSettings.AssemblyDrawingsGroup))
      {
        DrawingTypeSettings settings = this.driver.DrawingTypes.GetSettings(possibleType.Id);
        if (!string.IsNullOrEmpty(settings.StmName))
        {
          string str = StmFile.Locate(settings);
          if (!string.IsNullOrEmpty(str))
          {
            ValueBag firstNormalStamp;
            if (!pathDictionary.TryGetValue(str, out firstNormalStamp))
            {
              firstNormalStamp = DwgOperations.GetFirstNormalStamp(this.driver.Integrator, docItem, str, attributes, (Predicate<ValueBag>) (x => !string.IsNullOrEmpty(MechanicalLayer.ReadDesignType(x)) || DwgPredicates.StampIsValid(x)));
              pathDictionary.Add(str, firstNormalStamp);
            }
            string designType = MechanicalLayer.ReadDesignType(firstNormalStamp);
            if (!string.IsNullOrEmpty(designType))
            {
              List<LocalId<int>> localIdList = this.driver.FilterDocumentTypesByDesignType(docItem, (ICollection<LocalId<int>>) possibleTypes, designType);
              if (localIdList.Count > 0)
                return localIdList;
            }
          }
        }
      }
    }
    foreach (LocalId<int> possibleType in possibleTypes)
    {
      if (!(this.driver.DrawingTypes.GetGroupTypeByDrawingType(possibleType.Id, true) != MechanicalSettings.PartDrawingsGroup))
      {
        DrawingTypeSettings settings = this.driver.DrawingTypes.GetSettings(possibleType.Id);
        if (!string.IsNullOrEmpty(settings.StmName))
        {
          string str = StmFile.Locate(settings);
          if (!string.IsNullOrEmpty(str))
          {
            ValueBag firstNormalStamp;
            if (!pathDictionary.TryGetValue(str, out firstNormalStamp))
            {
              firstNormalStamp = DwgOperations.GetFirstNormalStamp(this.driver.Integrator, docItem, str, attributes, new Predicate<ValueBag>(DwgPredicates.StampIsValid));
              pathDictionary.Add(str, firstNormalStamp);
            }
            if (DwgPredicates.StampIsValid(firstNormalStamp))
            {
              List<LocalId<int>> all = possibleTypes.FindAll((Predicate<LocalId<int>>) (dwgType => this.driver.DrawingTypes.GetGroupTypeByDrawingType(dwgType.Id, true) == MechanicalSettings.PartDrawingsGroup));
              if (all.Count > 0)
                return all;
            }
          }
        }
      }
    }
    return (List<LocalId<int>>) null;
  }

  private static string ReadDesignType(ValueBag stampTable)
  {
    return stampTable.Read<string>((StringKey) Intermech.Localization.Localization.rm.GetString("EMB_DesignType"), (string) null);
  }

  public ContainerValues ReadDocumentProperties(SectionEntity docItem)
  {
    DrawingTypeSettings settings = this.driver.DrawingTypes.GetSettings(ObjectSection.GetObjectType(docItem));
    return DwgOperations.GetStamp(this.driver.Integrator, docItem, settings);
  }

  public bool WriteDocumentProperties(SectionEntity docItem, ContainerValues fileProperties)
  {
    if (!fileProperties.Bag.HasChanges)
      return false;
    throw new NotSupportedException("Интегратор не имеет возможности записать измененные атрибуты в файл чертежа.");
  }

  public void SaveDocumentFile(SectionEntity docItem)
  {
    CadApiService service = ServiceUtils.GetService<CadApiService>((object) this.driver.Integrator, true);
    if (!service.IsApplicationRunning)
      return;
    using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) service))
    {
      ICadDocumentProxy openDocument = acadApiSession.Application.FindOpenDocument(FilesSection.GetMasterFile(docItem));
      if (openDocument == null || !openDocument.Modified || openDocument.IsReadOnly)
        return;
      openDocument.Save();
    }
  }

  public ValueBag DecodeDocumentAttributes(SectionEntity docItem, ContainerValues fileProperties)
  {
    int objectType = ObjectSection.GetObjectType(docItem);
    ValueBag attributes = new ValueBag();
    foreach (ValueRecord valueRecord in fileProperties.Bag)
      this.EmitDocumentDecodeAction(valueRecord.Key, fileProperties, attributes, objectType).Perform();
    attributes.AcceptChanges();
    return attributes;
  }

  private IAction EmitDocumentDecodeAction(
    StringKey attributeKey,
    ContainerValues fileProperties,
    ValueBag attributes,
    int docType)
  {
    if (attributeKey == (StringKey) IDCache.Default.Designation.Text)
      return (IAction) new DecodeDocumentDesignationAction(fileProperties.Bag, attributeKey, attributes, attributeKey, docType);
    if (attributeKey == (StringKey) IDCache.Default.Name.Text)
    {
      DataTypeFilterAction typeFilterAction = new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey), typeof (string), true);
    }
    return (IAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey);
  }

  public void EncodeDocumentAttributes(
    SectionEntity docItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
  }

  public void ProcessDocumentAttributes(
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

  public string GetDocumentTypeAttributeName(SectionEntity docItem) => (string) null;

  public ICollection<StringKey> GetDocumentSyncAttributes(SectionEntity docItem)
  {
    return (ICollection<StringKey>) docItem.Sections.Get<AttributesSection>().WorkingSet.Keys;
  }

  public ValueBag TryReadDocumentRelationAttributes(
    SectionEntity projectDocument,
    SectionEntity partDocument)
  {
    return (ValueBag) null;
  }

  public List<string> GetSatelliteFiles(SectionEntity docItem)
  {
    if (this.driver.DrawingTypes.GetSettings(ObjectSection.GetObjectType(docItem)).XRefMode != XRefMode.AncillaryFiles)
      return new List<string>(0);
    string masterFile = FilesSection.GetMasterFile(docItem);
    List<string> liveXrefs = DwgOperations.GetLiveXRefs(this.driver.Integrator, masterFile);
    DwgOperations.FilterLiveXRefs(masterFile, liveXrefs);
    return liveXrefs;
  }

  public List<string> GetPrivateFiles(SectionEntity docItem) => new List<string>(0);

  public ICollection<InitialArticleData> ReadArticles(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    return this.driver.ArticleEmitter.EmitArticles(this.ctx, documentItem);
  }

  public ContainerValues ReadArticleProperties(SectionEntity articleItem)
  {
    return new ContainerValues(articleItem.Sections.Get<DwgArticleData>().FileProperties, false);
  }

  public bool WriteArticleProperties(SectionEntity articleItem, ContainerValues fileProperties)
  {
    if (!fileProperties.Bag.HasChanges)
      return false;
    throw new NotSupportedException("Интегратор не имеет возможности записать измененные атрибуты в файл чертежа.");
  }

  public ValueBag DecodeArticleAttributes(SectionEntity articleItem, ContainerValues fileProperties)
  {
    DecodeAttributesOptions decodeOptions = this.driver.MechanicalOperations.Articles.GetDecodeOptions(articleItem);
    ValueBag attributes = new ValueBag();
    foreach (ValueRecord valueRecord in fileProperties.Bag)
      this.EmitArticleDecodeAction(valueRecord.Key, fileProperties, attributes, DocumentAttributesOptions.TryGetDocumentTypeFromOptions((IAttributeCodecOptions) decodeOptions)).Perform();
    attributes.AcceptChanges();
    return attributes;
  }

  private IAction EmitArticleDecodeAction(
    StringKey attributeKey,
    ContainerValues fileProperties,
    ValueBag attributes,
    int docType)
  {
    if (attributeKey == (StringKey) IDCache.Default.OKPCode.Text)
      return (IAction) new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey), typeof (string), true);
    if (attributeKey == (StringKey) IDCache.Default.Name.Text)
      return (IAction) new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey), typeof (string), true);
    if (attributeKey == (StringKey) IDCache.Default.ImbaseKey.Text)
      return (IAction) new DataTypeFilterAction((TransferValueRecordAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey), typeof (string), false);
    if (attributeKey == (StringKey) IDCache.Default.Mass.Text)
      return (IAction) new DecodeMassAction(fileProperties.Bag, attributeKey, attributes, attributeKey);
    return attributeKey == (StringKey) IDCache.Default.Material.Text ? (IAction) new DecodeMaterialAction(fileProperties.Bag, attributeKey, attributes, attributeKey) : (IAction) new CopySourceValueAction(fileProperties.Bag, attributes, attributeKey);
  }

  public void EncodeArticleAttributes(
    SectionEntity articleItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
  }

  public ICollection<StringKey> GetArticleSyncAttributes(SectionEntity articleItem)
  {
    return (ICollection<StringKey>) articleItem.Sections.Get<AttributesSection>().WorkingSet.Keys;
  }
}
