// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AssemblyDwgArticleEmitter
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Collections;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AssemblyDwgArticleEmitter : IDwgArticleEmitter
{
  private MechanicalDriver driver;
  private IServiceProvider integrator;
  private DwgInputData inputData;
  private string baseProjectDesignation;
  private string baseProjectName;
  private List<string> projectDesignations;
  private StructFile structFile;
  private SpecDummy spec;

  public AssemblyDwgArticleEmitter(
    MechanicalDriver driver,
    IServiceProvider integrator,
    DwgInputData inputData)
  {
    if (driver == null)
      throw new ArgumentNullException();
    if (integrator == null)
      throw new ArgumentNullException();
    if (inputData == null)
      throw new ArgumentNullException();
    this.driver = driver;
    this.integrator = integrator;
    this.inputData = inputData;
  }

  public ICollection<InitialArticleData> EmitArticles(
    CaptureChangesDriverContext ctx,
    SectionEntity modelItem)
  {
    try
    {
      this.SetupBaseProject(modelItem);
      this.ParseStructFile(modelItem);
      this.CollectProjects();
      ICollection<InitialArticleData> initialArticleDatas = (ICollection<InitialArticleData>) new LinkedList<InitialArticleData>();
      this.EmitParts(modelItem, initialArticleDatas);
      this.EmitProjects(modelItem, initialArticleDatas);
      this.EmitProjectStructures(ctx, modelItem, (IEnumerable<InitialArticleData>) initialArticleDatas);
      return initialArticleDatas;
    }
    finally
    {
      this.Cleanup();
    }
  }

  private void SetupBaseProject(SectionEntity modelItem)
  {
    AttributesSection attributesSection = modelItem.Sections.Get<AttributesSection>();
    string origDesignation = attributesSection.WorkingSet.Read<string>((StringKey) IDCache.Default.Designation.Text, string.Empty);
    string str = attributesSection.WorkingSet.Read<string>((StringKey) IDCache.Default.Name.Text, string.Empty);
    this.baseProjectDesignation = DocumentDesignationHelper.RemoveDocCode(origDesignation, ObjectSection.GetObjectType(modelItem));
    this.baseProjectName = str;
    if (string.IsNullOrEmpty(this.baseProjectDesignation))
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_6"), (object) FilesSection.GetMasterFile(modelItem));
      stringBuilder.Append(' ');
      stringBuilder.Append(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_7"));
      throw new FaultException(stringBuilder.ToString());
    }
  }

  private void ParseStructFile(SectionEntity modelItem)
  {
    DataTable file = new StructFileParser().ParseFile(this.inputData.StructFileContent, this.inputData.FieldLayoutContent);
    CreateSpecJob job = new CreateSpecJob();
    job.ProcessingMode = StructFileProcessingModes.Cadmech;
    job.SuffixMode = true;
    job.BaseProjectDesignation = this.baseProjectDesignation;
    job.BaseProjectName = this.baseProjectName;
    this.structFile = new StructFileCodec(this.integrator).Decode(job, file, this.inputData.FieldLayoutContent);
    this.spec = new MappedUpdater().CreateSpecDummy(this.structFile);
    modelItem.Sections.Set((object) new DwgSpecData(this.structFile, this.spec));
  }

  private void CollectProjects()
  {
    this.projectDesignations = new List<string>(this.spec.Records.Count + 1);
    for (int index = 0; index < this.spec.Records.Count; ++index)
    {
      string projectDesignation = this.spec.Records[index].ProjectDesignation;
      if (projectDesignation != string.Empty && !this.projectDesignations.Contains(projectDesignation))
        this.projectDesignations.Add(projectDesignation);
    }
    if (this.projectDesignations.Contains(this.baseProjectDesignation))
      return;
    this.projectDesignations.Add(this.baseProjectDesignation);
  }

  private void EmitParts(SectionEntity modelItem, ICollection<InitialArticleData> articleBlanks)
  {
    for (int index = 0; index < this.spec.Parts.Count; ++index)
      this.EmitPart(this.spec.Parts[index], articleBlanks);
  }

  private void EmitPart(PartData partData, ICollection<InitialArticleData> articleBlanks)
  {
    if (partData.TaggingMode == TaggingModes.ImbaseKey)
    {
      this.EmitImbaseObject(partData, articleBlanks);
    }
    else
    {
      switch (partData.SectionCode)
      {
        case 'M':
          this.EmitMinorMaterialObject(partData, articleBlanks);
          break;
        case 'O':
          break;
        default:
          this.EmitNormalPartObject(partData, articleBlanks);
          break;
      }
    }
  }

  private void EmitImbaseObject(PartData partData, ICollection<InitialArticleData> articleBlanks)
  {
    DwgArticleData sectionObject = new DwgArticleData();
    sectionObject.FileProperties.Add((StringKey) IDCache.Default.ImbaseKey.Text, (object) partData.ImbaseKey);
    sectionObject.FileProperties.Add((StringKey) IDCache.Default.Name.Text, (object) partData.Name);
    sectionObject.FileProperties.SetFlagForAll(NamedFlags.ReadOnly);
    sectionObject.FileProperties.AcceptChanges();
    InitialArticleData initialArticleData = new InitialArticleData(MechanicalArticleKind.ImbaseObject);
    initialArticleData.DisplayName = partData.Name;
    initialArticleData.ArticleKey = AssemblyDwgArticleEmitter.GetPartArticleKey(partData);
    initialArticleData.CustomSections.Set((object) sectionObject);
    initialArticleData.CustomSections.Set((object) partData);
    articleBlanks.Add(initialArticleData);
  }

  private void EmitMinorMaterialObject(
    PartData partData,
    ICollection<InitialArticleData> articleBlanks)
  {
    DwgArticleData sectionObject = new DwgArticleData();
    sectionObject.FileProperties.Add((StringKey) IDCache.Default.Name.Text, (object) partData.Name);
    sectionObject.FileProperties.SetFlagForAll(NamedFlags.ReadOnly);
    sectionObject.FileProperties.AcceptChanges();
    InitialArticleData initialArticleData = new InitialArticleData(MechanicalArticleKind.MinorMaterial);
    initialArticleData.DisplayName = partData.Name;
    initialArticleData.ArticleKey = AssemblyDwgArticleEmitter.GetPartArticleKey(partData);
    initialArticleData.CustomSections.Set((object) sectionObject);
    initialArticleData.CustomSections.Set((object) partData);
    articleBlanks.Add(initialArticleData);
  }

  private void EmitNormalPartObject(
    PartData partData,
    ICollection<InitialArticleData> articleBlanks)
  {
    DwgArticleData dwgArticleData = new DwgArticleData();
    this.EmitPartParameters(dwgArticleData, partData);
    this.EmitPartPossibleTypes(dwgArticleData, partData);
    InitialArticleData initData = new InitialArticleData(MechanicalArticleKind.NormalArticle);
    initData.DisplayName = string.IsNullOrEmpty(partData.Designation) ? partData.Name : partData.Designation;
    initData.ArticleKey = AssemblyDwgArticleEmitter.GetPartArticleKey(partData);
    initData.CustomSections.Set((object) dwgArticleData);
    initData.CustomSections.Set((object) partData);
    this.TryConvertToReadOnlyArticle(partData, initData);
    articleBlanks.Add(initData);
  }

  private void EmitPartParameters(DwgArticleData customData, PartData partData)
  {
    customData.FileProperties.Add((StringKey) IDCache.Default.Designation.Text, (object) partData.Designation);
    customData.FileProperties.Add((StringKey) IDCache.Default.OKPCode.Text, (object) partData.OKP);
    customData.FileProperties.Add((StringKey) IDCache.Default.Name.Text, (object) partData.Name);
    MeasuredValue measuredValue = partData.Mass;
    if (measuredValue != null)
      measuredValue = (MeasuredValue) measuredValue.Clone();
    customData.FileProperties.Add((StringKey) IDCache.Default.Mass.Text, (object) measuredValue, typeof (MeasuredValue));
    object obj = Intermech.Consts.IsUndefinedObjectId(partData.MaterialId) ? (object) TypedNull.Int64 : (object) partData.MaterialId;
    customData.FileProperties.Add((StringKey) IDCache.Default.Material.Text, obj);
    customData.FileProperties.SetFlagForAll(NamedFlags.ReadOnly);
    customData.FileProperties.AcceptChanges();
  }

  private void EmitPartPossibleTypes(DwgArticleData customData, PartData partData)
  {
    customData.PossibleObjectTypes.Add(SpecSections.GetSectionObjectType(partData.SectionCode, partData.DocumentFormat));
  }

  private void TryConvertToReadOnlyArticle(PartData partData, InitialArticleData initData)
  {
    PartGuidArticleLocator guidArticleLocator = new PartGuidArticleLocator((IPartGuidArticleLocatorData) new PartGuidArticleLocatorData(partData.PartGuid));
    ObjectLocatorResult objectLocatorResult = guidArticleLocator.LocateObject();
    if (objectLocatorResult == null || DBDocumentHelper.FindArticleDocuments(objectLocatorResult.ObjectId, true, true, guidArticleLocator.VersionsRule).Count == 0)
      return;
    initData.ArticleKind = MechanicalArticleKind.ReadOnlyArticle;
    initData.ObjectId = objectLocatorResult.ObjectId;
  }

  private void EmitProjects(SectionEntity modelItem, ICollection<InitialArticleData> articleBlanks)
  {
    for (int index = 0; index < this.projectDesignations.Count; ++index)
    {
      string projectDesignation = this.projectDesignations[index];
      bool baseProject = projectDesignation == this.baseProjectDesignation;
      InitialArticleData initialArticleData = this.EmitProject(projectDesignation, modelItem);
      initialArticleData.CustomSections.Set((object) new DwgProjectData(baseProject));
      articleBlanks.Add(initialArticleData);
    }
  }

  private InitialArticleData EmitProject(string designation, SectionEntity modelItem)
  {
    DwgArticleData dwgArticleData = new DwgArticleData();
    this.EmitProjectParameters(dwgArticleData, designation);
    this.EmitProjectPossibleTypes(dwgArticleData, modelItem);
    InitialArticleData initialArticleData = new InitialArticleData(MechanicalArticleKind.NormalArticle);
    initialArticleData.DisplayName = designation;
    initialArticleData.ArticleKey = AssemblyDwgArticleEmitter.GetProjectArticleKey(designation);
    initialArticleData.InitialDocumentType = ArticleInitialDocumentType.Normal;
    initialArticleData.CustomSections.Set((object) dwgArticleData);
    return initialArticleData;
  }

  private void EmitProjectParameters(DwgArticleData customData, string designation)
  {
    customData.FileProperties.Add((StringKey) IDCache.Default.Designation.Text, (object) designation);
    customData.FileProperties.Add((StringKey) IDCache.Default.OKPCode.Text, (object) string.Empty);
    customData.FileProperties.Add((StringKey) IDCache.Default.Name.Text, (object) this.baseProjectName);
    customData.FileProperties.SetFlagForAll(NamedFlags.ReadOnly);
    customData.FileProperties.AcceptChanges();
  }

  private void EmitProjectPossibleTypes(DwgArticleData customData, SectionEntity modelItem)
  {
    List<LocalId<int>> possibleArticleTypes = this.driver.MechanicalOperations.Articles.GetPossibleArticleTypes(ObjectSection.GetObjectType(modelItem));
    customData.PossibleObjectTypes.AddRange((IEnumerable<LocalId<int>>) possibleArticleTypes);
  }

  private void EmitProjectStructures(
    CaptureChangesDriverContext ctx,
    SectionEntity modelItem,
    IEnumerable<InitialArticleData> initData)
  {
    for (int index = 0; index < this.spec.Records.Count; ++index)
    {
      SpecRecord record = this.spec.Records[index];
      string partComponentKey = AssemblyDwgArticleEmitter.GetPartArticleKey(record.Part);
      InitialArticleData initialArticleData1 = CollectionUtils.Find<InitialArticleData>(initData, (Predicate<InitialArticleData>) (item => PathUtils.IsSamePath(item.ArticleKey, partComponentKey)));
      foreach (string projectArticleKey in this.GetProjectArticleKeys(record))
      {
        string projArticleKey = projectArticleKey;
        InitialArticleData initialArticleData2 = CollectionUtils.Find<InitialArticleData>(initData, (Predicate<InitialArticleData>) (item => PathUtils.IsSamePath(item.ArticleKey, projArticleKey)));
        ArticleStructureOccurence structureOccurence = new ArticleStructureOccurence(record.Part.PartGuid, initialArticleData1.ArticleKey);
        structureOccurence.Attributes.Add((StringKey) IDCache.Default.Count.Text, (object) record.Count);
        structureOccurence.Attributes.Add((StringKey) IDCache.Default.Zone.Text, (object) record.Zone);
        structureOccurence.Attributes.Add((StringKey) IDCache.Default.Note.Text, (object) record.Note);
        structureOccurence.Attributes.Add((StringKey) IDCache.Default.Position.Text, (object) record.Position);
        structureOccurence.Attributes.SetFlagForAll(NamedFlags.ReadOnly);
        SpecRelation sectionObject = new SpecRelation();
        record.Relations.Add(sectionObject);
        structureOccurence.Sections.Set((object) sectionObject);
        initialArticleData2.CustomSections.Get<DwgArticleData>().Structure.Add(structureOccurence);
      }
    }
  }

  private List<string> GetProjectArticleKeys(SpecRecord specRecord)
  {
    List<string> projectArticleKeys = new List<string>(this.projectDesignations.Count);
    if (specRecord.ProjectDesignation == string.Empty)
    {
      foreach (string projectDesignation in this.projectDesignations)
        projectArticleKeys.Add(AssemblyDwgArticleEmitter.GetProjectArticleKey(projectDesignation));
    }
    else
      projectArticleKeys.Add(AssemblyDwgArticleEmitter.GetProjectArticleKey(specRecord.ProjectDesignation));
    return projectArticleKeys;
  }

  private static string GetProjectArticleKey(string projectDesignation) => projectDesignation;

  private static string GetPartArticleKey(PartData partData) => partData.PartGuid.ToString("N");

  private void Cleanup()
  {
    this.baseProjectDesignation = (string) null;
    this.baseProjectName = (string) null;
    this.structFile = (StructFile) null;
    this.spec = (SpecDummy) null;
    this.projectDesignations = (List<string>) null;
  }
}
