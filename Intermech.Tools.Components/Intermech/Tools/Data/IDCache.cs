// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.IDCache
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Data;

public class IDCache
{
  private static readonly ApplicationServiceRef<IDCache> defaultInstanceRef = new ApplicationServiceRef<IDCache>();
  private MetadataResolverFactory metadataResolvers;

  public IDCache(MetadataResolverFactory metadataResolvers)
  {
    this.metadataResolvers = metadataResolvers != null ? metadataResolvers : throw new ArgumentNullException(nameof (metadataResolvers));
    this.InitializeCommonAttributeResolvers();
    this.InitializeCommonObjectTypeResolvers();
    this.InitializeCommonRelationTypeResolvers();
    this.InitializeCommonSpecialObjectResolvers();
    this.InitializeAlternativeRepresenationsResolvers();
  }

  private MetadataResolverFactory MetadataResolvers
  {
    [DebuggerStepThrough] get => this.metadataResolvers;
  }

  public AttributeTypeResolver ContentModifyDate { get; private set; }

  public AttributeTypeResolver NormalizedId { get; private set; }

  public AttributeTypeResolver Designation { get; private set; }

  public AttributeTypeResolver OKPCode { get; private set; }

  public AttributeTypeResolver Name { get; private set; }

  public AttributeTypeResolver ImbaseKey { get; private set; }

  public AttributeTypeResolver ImbaseRef { get; private set; }

  public AttributeTypeResolver InstanceGroupId { get; private set; }

  public AttributeTypeResolver Format { get; private set; }

  public AttributeTypeResolver Note { get; private set; }

  public AttributeTypeResolver ImbaseTable { get; private set; }

  public AttributeTypeResolver ImbaseRecord { get; private set; }

  public AttributeTypeResolver BasedOnCADModel { get; private set; }

  public AttributeTypeResolver ObjectExternalKey { get; private set; }

  public AttributeTypeResolver OccurenceKey { get; private set; }

  public AttributeTypeResolver CADConfigurationFile { get; private set; }

  public AttributeTypeResolver CADConfigurationName { get; private set; }

  public AttributeTypeResolver Zone { get; private set; }

  public AttributeTypeResolver Dimensions { get; private set; }

  public AttributeTypeResolver Position { get; private set; }

  public AttributeTypeResolver Count { get; private set; }

  public AttributeTypeResolver Mass { get; private set; }

  public AttributeTypeResolver SubstitutionGroup { get; private set; }

  public AttributeTypeResolver SubstitutionNumber { get; private set; }

  public AttributeTypeResolver FixedRelation { get; private set; }

  public AttributeTypeResolver FixedRelationMode { get; private set; }

  public AttributeTypeResolver PrivateFiles { get; private set; }

  public AttributeTypeResolver Material { get; private set; }

  public AttributeTypeResolver MaterialReplacement1 { get; private set; }

  public AttributeTypeResolver MaterialReplacement2 { get; private set; }

  public AttributeTypeResolver OwnedByIntegrator { get; private set; }

  public AttributeTypeResolver RequireIdentityCheck { get; private set; }

  public AttributeTypeResolver RequireTypeCheck { get; private set; }

  public AttributeTypeResolver RequireFileCheck { get; private set; }

  public AttributeTypeResolver CADLinkType { get; private set; }

  public AttributeTypeResolver Scale { get; private set; }

  public AttributeTypeResolver NumberOfSheets { get; private set; }

  public AttributeTypeResolver LetterOfSheet { get; private set; }

  public AttributeTypeResolver IntegrationStatus { get; private set; }

  public AttributeTypeResolver IntegrationErrors { get; private set; }

  public AttributeTypeResolver PosDesignation { get; private set; }

  public AttributeTypeResolver PDMConfigCriteria { get; private set; }

  public AttributeTypeResolver PDMConfigContext { get; private set; }

  private void InitializeCommonAttributeResolvers()
  {
    this.ContentModifyDate = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0013A-306C-11D8-B4E9-00304F19F545"));
    this.NormalizedId = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0011A-306C-11D8-B4E9-00304F19F545"));
    this.Designation = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0001F-306C-11D8-B4E9-00304F19F545"));
    this.OKPCode = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0038A-306C-11D8-B4E9-00304F19F545"));
    this.Name = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00020-306C-11D8-B4E9-00304F19F545"));
    this.ImbaseKey = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00162-306C-11D8-B4E9-00304F19F545"));
    this.ImbaseRef = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00209-306C-11D8-B4E9-00304F19F545"));
    this.InstanceGroupId = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD001F9-306C-11D8-B4E9-00304F19F545"));
    this.Format = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00255-306C-11D8-B4E9-00304F19F545"));
    this.Note = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00021-306C-11D8-B4E9-00304F19F545"));
    this.ImbaseTable = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00209-306C-11D8-B4E9-00304F19F545"));
    this.ImbaseRecord = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0020F-306C-11D8-B4E9-00304F19F545"));
    this.BasedOnCADModel = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0153E-306C-11D8-B4E9-00304F19F545"));
    this.ObjectExternalKey = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00378-306C-11D8-B4E9-00304F19F545"));
    this.OccurenceKey = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0027B-306C-11D8-B4E9-00304F19F545"));
    this.CADConfigurationFile = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD014B4-306C-11D8-B4E9-00304F19F545"));
    this.CADConfigurationName = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD95AF-306C-11D8-B4E9-00304F19F545"));
    this.Zone = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0027A-306C-11D8-B4E9-00304F19F545"));
    this.Dimensions = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00277-306C-11D8-B4E9-00304F19F545"));
    this.Position = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00270-306C-11D8-B4E9-00304F19F545"));
    this.Count = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00267-306C-11D8-B4E9-00304F19F545"));
    this.Mass = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD00275-306C-11D8-B4E9-00304F19F545"));
    this.SubstitutionGroup = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD001C0-306C-11D8-B4E9-00304F19F545"));
    this.SubstitutionNumber = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD001C1-306C-11D8-B4E9-00304F19F545"));
    this.FixedRelation = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD001C2-306C-11D8-B4E9-00304F19F545"));
    this.FixedRelationMode = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD9609-306C-11D8-B4E9-00304F19F545"));
    this.PrivateFiles = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD9391-306C-11D8-B4E9-00304F19F545"));
    this.Material = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0038C-306C-11D8-B4E9-00304F19F545"));
    this.MaterialReplacement1 = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD94C2-306C-11D8-B4E9-00304F19F545"));
    this.MaterialReplacement2 = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD94C3-306C-11D8-B4E9-00304F19F545"));
    this.OwnedByIntegrator = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD94C4-306C-11D8-B4E9-00304F19F545"));
    this.RequireIdentityCheck = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD93F6-306C-11D8-B4E9-00304F19F545"));
    this.RequireTypeCheck = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD93F5-306C-11D8-B4E9-00304F19F545"));
    this.RequireFileCheck = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD9412-306C-11D8-B4E9-00304F19F545"));
    this.CADLinkType = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD94DA-306C-11D8-B4E9-00304F19F545"));
    this.Scale = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD003A8-306C-11D8-B4E9-00304F19F545"));
    this.NumberOfSheets = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD003A7-306C-11D8-B4E9-00304F19F545"));
    this.LetterOfSheet = this.MetadataResolvers.AttributeTypeResolver(new Guid("CAD0038B-306C-11D8-B4E9-00304F19F545"));
    this.IntegrationStatus = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD9735-306C-11D8-B4E9-00304F19F545"));
    this.IntegrationErrors = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD9739-306C-11D8-B4E9-00304F19F545"));
    this.PosDesignation = this.MetadataResolvers.AttributeTypeResolver(new Guid("cad01478-306c-11d8-b4e9-00304f19f545"));
    this.PDMConfigCriteria = this.MetadataResolvers.AttributeTypeResolver(new Guid("cad015ac-306c-11d8-b4e9-00304f19f545"));
    this.PDMConfigContext = this.MetadataResolvers.AttributeTypeResolver(new Guid("cad015a6-306c-11d8-b4e9-00304f19f545"));
  }

  public ObjectTypeResolver AllDocuments { get; private set; }

  public ObjectTypeResolver MechanicalDocuments { get; private set; }

  public ObjectTypeResolver PartDocuments { get; private set; }

  public ObjectTypeResolver AssemblyDocuments { get; private set; }

  public ObjectTypeResolver OtherDocuments { get; private set; }

  public ObjectTypeResolver StandardPartDocuments { get; private set; }

  public ObjectTypeResolver DrawinglessPart { get; private set; }

  public ObjectTypeResolver AllArticles { get; private set; }

  public ObjectTypeResolver StandardArticles { get; private set; }

  public ObjectTypeResolver AssistiveArticles { get; private set; }

  public ObjectTypeResolver AllMaterials { get; private set; }

  public ObjectTypeResolver UndefinedMaterial { get; private set; }

  private void InitializeCommonObjectTypeResolvers()
  {
    this.AllDocuments = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD00070-306C-11D8-B4E9-00304F19F545"));
    this.MechanicalDocuments = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD0057F-306C-11D8-B4E9-00304F19F545"));
    this.PartDocuments = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD0078F-306C-11D8-B4E9-00304F19F545"));
    this.AssemblyDocuments = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD00768-306C-11D8-B4E9-00304F19F545"));
    this.OtherDocuments = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD0082C-306C-11D8-B4E9-00304F19F545"));
    this.StandardPartDocuments = this.MetadataResolvers.ObjectTypeResolver(new Guid("cad015cb-306c-11d8-b4e9-00304f19f545"));
    this.DrawinglessPart = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD00861-306C-11D8-B4E9-00304F19F545"));
    this.AllArticles = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD00268-306C-11D8-B4E9-00304F19F545"));
    this.StandardArticles = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD00252-306C-11D8-B4E9-00304F19F545"));
    this.AssistiveArticles = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD0038D-306C-11D8-B4E9-00304F19F545"));
    this.AllMaterials = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD00170-306C-11D8-B4E9-00304F19F545"));
    this.UndefinedMaterial = this.MetadataResolvers.ObjectTypeResolver(new Guid("CAD0081D-306C-11D8-B4E9-00304F19F545"));
  }

  public RelationTypeResolver ArticleToDocumentTree { get; private set; }

  public RelationTypeResolver ArticleTree { get; private set; }

  public RelationTypeResolver DocumentTree { get; private set; }

  private void InitializeCommonRelationTypeResolvers()
  {
    this.ArticleToDocumentTree = this.MetadataResolvers.RelationTypeResolver(new Guid("CAD00154-306C-11D8-B4E9-00304F19F545"));
    this.ArticleTree = this.MetadataResolvers.RelationTypeResolver(new Guid("CAD00023-306C-11D8-B4E9-00304F19F545"));
    this.DocumentTree = this.MetadataResolvers.RelationTypeResolver(new Guid("CAD0057C-306C-11D8-B4E9-00304F19F545"));
  }

  public SpecialObjectResolver ItemsMeasure { get; private set; }

  public SpecialObjectResolver KilogramMeasure { get; private set; }

  public SpecialObjectResolver GramMeasure { get; private set; }

  public SpecialObjectResolver TonMeasure { get; private set; }

  public SpecialObjectResolver PoundMeasure { get; private set; }

  public SpecialObjectResolver MassPhysQty { get; private set; }

  private void InitializeCommonSpecialObjectResolvers()
  {
    this.ItemsMeasure = this.MetadataResolvers.SpecialObjectResolver(new Guid("CAD002E8-306C-11D8-B4E9-00304F19F545"));
    this.KilogramMeasure = this.MetadataResolvers.SpecialObjectResolver(new Guid("CAD002EB-306C-11D8-B4E9-00304F19F545"));
    this.GramMeasure = this.MetadataResolvers.SpecialObjectResolver(new Guid("CAD002EA-306C-11D8-B4E9-00304F19F545"));
    this.TonMeasure = this.MetadataResolvers.SpecialObjectResolver(new Guid("CAD002EE-306C-11D8-B4E9-00304F19F545"));
    this.PoundMeasure = this.MetadataResolvers.SpecialObjectResolver(new Guid("CAD014AA-306C-11D8-B4E9-00304F19F545"));
    this.MassPhysQty = this.MetadataResolvers.SpecialObjectResolver(new Guid("CAD002E9-306C-11D8-B4E9-00304F19F545"));
  }

  public ObjectTypeResolver AlternativeRepresenations { get; private set; }

  public ObjectTypeResolver JTDocuments { get; private set; }

  public AttributeTypeResolver JTSourceDocumentReference { get; private set; }

  public AttributeTypeResolver JTSourceDocumentMarker { get; private set; }

  private void InitializeAlternativeRepresenationsResolvers()
  {
    this.AlternativeRepresenations = this.MetadataResolvers.ObjectTypeResolver(new Guid("CADD94E8-306C-11D8-B4E9-00304F19F545"));
    this.JTDocuments = this.MetadataResolvers.ObjectTypeResolver(new Guid("CADD94E9-306C-11D8-B4E9-00304F19F545"));
    this.JTSourceDocumentReference = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD94EB-306C-11D8-B4E9-00304F19F545"));
    this.JTSourceDocumentMarker = this.MetadataResolvers.AttributeTypeResolver(new Guid("CADD94F5-306C-11D8-B4E9-00304F19F545"));
  }

  public static IDCache Default
  {
    [DebuggerStepThrough] get => IDCache.defaultInstanceRef.Value;
    [DebuggerStepThrough] set => IDCache.defaultInstanceRef.Value = value;
  }
}
