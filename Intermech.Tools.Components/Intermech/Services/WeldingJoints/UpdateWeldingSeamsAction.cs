// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.UpdateWeldingSeamsAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Experimental.Kernel.Entities;
using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using Intermech.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

#nullable disable
namespace Intermech.Services.WeldingJoints;

internal sealed class UpdateWeldingSeamsAction : IAction
{
  private long documentId;
  private IIntegrator integrator;
  private IMainFormUpdate mainFormService;
  private INotificationService notificationService;
  private IFileVault fileVaultService;
  private IWeldingSeamsModelRoot modelRoot;
  private UpdateWeldingSeamsResult result;
  private VersionsRulePackage versionsRule;
  private List<long> documentsWithoutArticles;

  public UpdateWeldingSeamsAction(
    long documentId,
    IIntegrator integrator,
    IMainFormUpdate mainFormService,
    INotificationService notificationService,
    IFileVault fileVaultService,
    IWeldingSeamsModelRoot modelRoot)
  {
    if (documentId == 0L)
      throw new ArgumentException("Не задан идентификатор версии документа IPS.", nameof (documentId));
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (mainFormService == null)
      throw new ArgumentNullException(nameof (mainFormService));
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (modelRoot == null)
      throw new ArgumentNullException(nameof (modelRoot));
    this.documentId = documentId;
    this.integrator = integrator;
    this.mainFormService = mainFormService;
    this.notificationService = notificationService;
    this.fileVaultService = fileVaultService;
    this.modelRoot = modelRoot;
  }

  public UpdateWeldingSeamsResult Result
  {
    [DebuggerStepThrough] get => this.result;
  }

  public void Perform()
  {
    try
    {
      this.InitializeAction();
      this.PerformInternal();
      this.result = this.CreateResultInternal();
    }
    finally
    {
      this.CleanupAction();
    }
  }

  private UpdateWeldingSeamsResult CreateResultInternal()
  {
    return this.documentsWithoutArticles.Count != 0 ? new UpdateWeldingSeamsResult((IReadOnlyList<long>) this.documentsWithoutArticles.ToArray()) : new UpdateWeldingSeamsResult((IReadOnlyList<long>) new long[0]);
  }

  private void InitializeAction()
  {
    this.versionsRule = VersionsRuleSources.GetEditorRule();
    this.documentsWithoutArticles = new List<long>();
  }

  private void CleanupAction()
  {
    this.versionsRule = (VersionsRulePackage) null;
    this.documentsWithoutArticles = (List<long>) null;
  }

  private void PerformInternal()
  {
    MechanicalDocumentEntity document = this.modelRoot.Documents.Load((object) this.documentId);
    List<MechanicalArticleEntity> articles = this.modelRoot.SpecialQueries.LoadLinkedArticles(document, this.versionsRule);
    if (articles.Count == 0)
    {
      this.documentsWithoutArticles.Add(this.documentId);
    }
    else
    {
      foreach (MechanicalArticleEntity entity in articles)
      {
        this.modelRoot.Articles.LoadReferences<List<WeldingSeamOccurence>>(entity, (Expression<Func<MechanicalArticleEntity, List<WeldingSeamOccurence>>>) (e => e.WeldingSeams));
        entity.WeldingSeams.RemoveAll((Predicate<WeldingSeamOccurence>) (occurence => !occurence.WeldingSeam.BasedOnCADModel));
        foreach (WeldingSeamOccurence weldingSeam in entity.WeldingSeams)
          this.modelRoot.WeldingSeams.LoadReferences<List<WeldingSeamComponentOccurence>>(weldingSeam.WeldingSeam, (Expression<Func<WeldingSeamEntity, List<WeldingSeamComponentOccurence>>>) (e => e.Components));
      }
      List<WeldingSeamExternalData> seamExternalDataList = this.ReadWeldingSeamsFromDocumentFile();
      List<UpdateWeldingSeamsAction.WeldingSeamMapping> weldingSeamMappings = new List<UpdateWeldingSeamsAction.WeldingSeamMapping>();
      foreach (WeldingSeamExternalData externalData in seamExternalDataList)
      {
        string weldingSeamExternalKey = document.CreateWeldingSeamExternalKey(externalData.AnchorGuid, externalData.IsOnBackSide);
        WeldingSeamEntity weldingSeam = this.modelRoot.SpecialQueries.LoadWeldingSeamByExternalKey(weldingSeamExternalKey, this.versionsRule, false);
        if (weldingSeam == null)
          weldingSeamMappings.Add(new UpdateWeldingSeamsAction.WeldingSeamMapping(new WeldingSeamEntity()
          {
            ExternalKey = weldingSeamExternalKey,
            BasedOnCADModel = true
          }, true, weldingSeamExternalKey, externalData));
        else
          weldingSeamMappings.Add(new UpdateWeldingSeamsAction.WeldingSeamMapping(weldingSeam, false, weldingSeamExternalKey, externalData));
      }
      foreach (UpdateWeldingSeamsAction.WeldingSeamMapping weldingSeamMapping in weldingSeamMappings)
        this.CreateWeldingSeamComponentMap(weldingSeamMapping);
      if (this.documentsWithoutArticles.Count != 0)
        return;
      SaveChangesUINotificationsBuilder notificationsBuilder = new SaveChangesUINotificationsBuilder((IModelRoot) this.modelRoot);
      using (IEntityBatchUpdateScope batchUpdateScope = this.modelRoot.StartBatchUpdate())
      {
        batchUpdateScope.UpdateLog = (IEntityBatchUpdateLog) notificationsBuilder;
        foreach (UpdateWeldingSeamsAction.WeldingSeamMapping weldingSeamMapping in weldingSeamMappings)
        {
          weldingSeamMapping.WeldingSeam.Number = weldingSeamMapping.ExternalData.Number;
          weldingSeamMapping.WeldingSeam.StandardName = weldingSeamMapping.ExternalData.StandardName;
          weldingSeamMapping.WeldingSeam.DesignationByStandard = weldingSeamMapping.ExternalData.DesignationByStandard;
          weldingSeamMapping.WeldingSeam.WeldingMethodDesignationByStandard = weldingSeamMapping.ExternalData.WeldingMethodDesignationByStandard;
          weldingSeamMapping.WeldingSeam.LegSizeByStandard = weldingSeamMapping.ExternalData.LegSizeByStandard;
          weldingSeamMapping.WeldingSeam.LegLowerTolerance = weldingSeamMapping.ExternalData.LegLowerTolerance;
          weldingSeamMapping.WeldingSeam.LegUpperTolerance = weldingSeamMapping.ExternalData.LegUpperTolerance;
          weldingSeamMapping.WeldingSeam.ExtraDimensions = weldingSeamMapping.ExternalData.ExtraDimensions;
          weldingSeamMapping.WeldingSeam.ControlComplexDesignation = weldingSeamMapping.ExternalData.ControlComplexDesignation;
          weldingSeamMapping.WeldingSeam.GeometryType = this.ConvertToString(weldingSeamMapping.ExternalData.GeometryType);
          weldingSeamMapping.WeldingSeam.FullLength = weldingSeamMapping.ExternalData.FullLength;
          weldingSeamMapping.WeldingSeam.LeftOffset = weldingSeamMapping.ExternalData.LeftOffset;
          weldingSeamMapping.WeldingSeam.RightOffset = weldingSeamMapping.ExternalData.RightOffset;
          weldingSeamMapping.WeldingSeam.SegmentationType = this.ConvertToString(weldingSeamMapping.ExternalData.SegmentationType);
          weldingSeamMapping.WeldingSeam.SegmentStep = weldingSeamMapping.ExternalData.SegmentStep;
          weldingSeamMapping.WeldingSeam.SegmentLength = weldingSeamMapping.ExternalData.SegmentLength;
          weldingSeamMapping.WeldingSeam.Gap = weldingSeamMapping.ExternalData.Gap;
          weldingSeamMapping.WeldingSeam.FirstPartThickness = weldingSeamMapping.ExternalData.FirstPartThickness;
          weldingSeamMapping.WeldingSeam.SecondPartThickness = weldingSeamMapping.ExternalData.SecondPartThickness;
          weldingSeamMapping.WeldingSeam.ConnectionKind = weldingSeamMapping.ExternalData.ConnectionKind;
          weldingSeamMapping.WeldingSeam.MakeAtInstallationStage = weldingSeamMapping.ExternalData.MakeAtInstallationStage;
          weldingSeamMapping.WeldingSeam.MakeClosed = weldingSeamMapping.ExternalData.MakeClosed;
          weldingSeamMapping.WeldingSeam.RemoveReinforcementOnFrontSide = weldingSeamMapping.ExternalData.RemoveReinforcementOnFrontSide;
          weldingSeamMapping.WeldingSeam.ProcessIrregularitiesOnFrontSide = weldingSeamMapping.ExternalData.ProcessIrregularitiesOnFrontSide;
          weldingSeamMapping.WeldingSeam.MakeOpenOnFrontSide = weldingSeamMapping.ExternalData.MakeOpenOnFrontSide;
          weldingSeamMapping.WeldingSeam.Note = weldingSeamMapping.ExternalData.Note;
          weldingSeamMapping.WeldingSeam.Length = weldingSeamMapping.ExternalData.Length;
          if (weldingSeamMapping.ExternalData.RemoveReinforcementOnBackSide.HasValue)
            weldingSeamMapping.WeldingSeam.RemoveReinforcementOnBackSide = weldingSeamMapping.ExternalData.RemoveReinforcementOnBackSide;
          if (weldingSeamMapping.ExternalData.ProcessIrregularitiesOnBackSide.HasValue)
            weldingSeamMapping.WeldingSeam.ProcessIrregularitiesOnBackSide = weldingSeamMapping.ExternalData.ProcessIrregularitiesOnBackSide;
          if (weldingSeamMapping.ExternalData.MakeOpenOnBackSide.HasValue)
            weldingSeamMapping.WeldingSeam.MakeOpenOnBackSide = weldingSeamMapping.ExternalData.MakeOpenOnBackSide;
          if (weldingSeamMapping.WeldingSeam.DxfSketch.Equals(DBFileValue.Empty) && weldingSeamMapping.ExternalData.DxfSketch.Length != 0 || !CollectionUtils.ContentEqual<byte>((ICollection<byte>) weldingSeamMapping.WeldingSeam.DxfSketch.Content, (ICollection<byte>) weldingSeamMapping.ExternalData.DxfSketch))
          {
            string name = weldingSeamMapping.WeldingSeam.DxfSketch.Name;
            if (string.IsNullOrEmpty(name))
              name = Guid.NewGuid().ToString("n") + ".dxf";
            weldingSeamMapping.WeldingSeam.DxfSketch = new DBFileValue(name, weldingSeamMapping.ExternalData.DxfSketch);
          }
        }
        this.RemoveTotallyUnusedWeldingSeams(articles, weldingSeamMappings);
        this.LinkUsedWeldingSeams(articles, weldingSeamMappings);
        this.UnlinkUnusedWeldingSeams(articles, weldingSeamMappings);
        this.SynchronizeWeldingSeamComponents(articles, weldingSeamMappings);
        batchUpdateScope.Complete();
      }
      List<NotificationEventArgs> notificationList = notificationsBuilder.ToNotificationList();
      if (notificationList.Count == 0)
        return;
      foreach (NotificationEventArgs e in notificationList)
        this.notificationService.FireEvent((object) this, e);
    }
  }

  private void CreateWeldingSeamComponentMap(
    UpdateWeldingSeamsAction.WeldingSeamMapping weldingSeamMapping)
  {
    foreach (WeldingSeamComponent component in weldingSeamMapping.ExternalData.Components)
    {
      long? documentIdByPath = this.TryGetDocumentIdByPath(component);
      if (documentIdByPath.HasValue)
      {
        if (string.IsNullOrEmpty(component.ArticleExternalKey))
        {
          this.documentsWithoutArticles.Add(documentIdByPath.Value);
        }
        else
        {
          MechanicalArticleEntity article = this.modelRoot.SpecialQueries.LoadWeldingSeamComponentByExternalKeys(documentIdByPath.Value, component.ArticleExternalKey, false);
          if (article == null)
            this.documentsWithoutArticles.Add(documentIdByPath.Value);
          else
            weldingSeamMapping.ComponentMap.Add(new UpdateWeldingSeamsAction.WeldingSeamComponentMapping(component, article));
        }
      }
    }
  }

  private void RemoveTotallyUnusedWeldingSeams(
    List<MechanicalArticleEntity> articles,
    List<UpdateWeldingSeamsAction.WeldingSeamMapping> weldingSeamMappings)
  {
    List<WeldingSeamEntity> doomedSeams = (List<WeldingSeamEntity>) null;
    foreach (MechanicalArticleEntity article in articles)
    {
      foreach (WeldingSeamOccurence weldingSeam in article.WeldingSeams)
      {
        WeldingSeamOccurence seamOccurence = weldingSeam;
        if (!weldingSeamMappings.Exists((Predicate<UpdateWeldingSeamsAction.WeldingSeamMapping>) (seamMapping => seamMapping.ExternalKey == seamOccurence.WeldingSeam.ExternalKey)))
        {
          if (doomedSeams == null)
            doomedSeams = new List<WeldingSeamEntity>();
          if (!doomedSeams.Contains(seamOccurence.WeldingSeam))
            doomedSeams.Add(seamOccurence.WeldingSeam);
        }
      }
    }
    if (doomedSeams == null)
      return;
    foreach (MechanicalArticleEntity article in articles)
      article.WeldingSeams.RemoveAll((Predicate<WeldingSeamOccurence>) (seamOccurence => doomedSeams.Contains(seamOccurence.WeldingSeam)));
    foreach (object entity in doomedSeams)
      this.modelRoot.ChangeTracker.MarkToRemove(entity);
  }

  private void LinkUsedWeldingSeams(
    List<MechanicalArticleEntity> articles,
    List<UpdateWeldingSeamsAction.WeldingSeamMapping> weldingSeamMappings)
  {
    foreach (UpdateWeldingSeamsAction.WeldingSeamMapping weldingSeamMapping in weldingSeamMappings)
    {
      UpdateWeldingSeamsAction.WeldingSeamMapping seamMapping = weldingSeamMapping;
      foreach (string configurationName in seamMapping.ExternalData.ConfigurationNames)
      {
        string cadConfigurationName = configurationName;
        MechanicalArticleEntity article1 = articles.Find((Predicate<MechanicalArticleEntity>) (article => article.DocumentOccurence.CADConfigurationName == cadConfigurationName));
        if (article1 != null)
        {
          WeldingSeamOccurence weldingSeamOccurence = article1.WeldingSeams.Find((Predicate<WeldingSeamOccurence>) (item => item.WeldingSeam.ExternalKey == seamMapping.ExternalKey));
          if (weldingSeamOccurence == null)
          {
            weldingSeamOccurence = new WeldingSeamOccurence(article1, seamMapping.WeldingSeam);
            article1.WeldingSeams.Add(weldingSeamOccurence);
          }
          weldingSeamOccurence.Count = this.CreateCountMeasuredValue(seamMapping.ExternalData.Count);
        }
      }
    }
  }

  private void UnlinkUnusedWeldingSeams(
    List<MechanicalArticleEntity> articles,
    List<UpdateWeldingSeamsAction.WeldingSeamMapping> weldingSeamMappings)
  {
    foreach (MechanicalArticleEntity article in articles)
    {
      string cadConfigurationName = article.DocumentOccurence.CADConfigurationName;
      article.WeldingSeams.RemoveAll((Predicate<WeldingSeamOccurence>) (seamOccurence =>
      {
        UpdateWeldingSeamsAction.WeldingSeamMapping weldingSeamMapping = weldingSeamMappings.Find((Predicate<UpdateWeldingSeamsAction.WeldingSeamMapping>) (seamMapping => seamMapping.ExternalKey == seamOccurence.WeldingSeam.ExternalKey));
        return weldingSeamMapping == null || !weldingSeamMapping.ExternalData.ConfigurationNames.Contains(cadConfigurationName);
      }));
    }
  }

  private void SynchronizeWeldingSeamComponents(
    List<MechanicalArticleEntity> articles,
    List<UpdateWeldingSeamsAction.WeldingSeamMapping> weldingSeamMappings)
  {
    foreach (UpdateWeldingSeamsAction.WeldingSeamMapping weldingSeamMapping in weldingSeamMappings)
    {
      WeldingSeamEntity weldingSeam = weldingSeamMapping.WeldingSeam;
      if (weldingSeamMapping.IsNewSeam && weldingSeam.Components == null)
        weldingSeam.Components = new List<WeldingSeamComponentOccurence>();
      foreach (UpdateWeldingSeamsAction.WeldingSeamComponentMapping component in weldingSeamMapping.ComponentMap)
      {
        UpdateWeldingSeamsAction.WeldingSeamComponentMapping mapItem = component;
        WeldingSeamComponentOccurence componentOccurence = weldingSeam.Components.Find((Predicate<WeldingSeamComponentOccurence>) (x => x.Article == mapItem.Article));
        if (componentOccurence == null)
        {
          componentOccurence = new WeldingSeamComponentOccurence(weldingSeam, mapItem.Article);
          weldingSeam.Components.Add(componentOccurence);
        }
        componentOccurence.GroupId = (long) mapItem.Component.GroupId;
      }
      List<WeldingSeamComponentOccurence> componentOccurenceList = new List<WeldingSeamComponentOccurence>();
      foreach (WeldingSeamComponentOccurence component in weldingSeam.Components)
      {
        WeldingSeamComponentOccurence componentOccurence = component;
        if (weldingSeamMapping.ComponentMap.Find((Predicate<UpdateWeldingSeamsAction.WeldingSeamComponentMapping>) (x => x.Article == componentOccurence.Article)) == null)
          componentOccurenceList.Add(componentOccurence);
      }
      foreach (WeldingSeamComponentOccurence componentOccurence in componentOccurenceList)
        weldingSeam.Components.Remove(componentOccurence);
    }
  }

  private MeasuredValue CreateCountMeasuredValue(string countValue)
  {
    if (string.IsNullOrEmpty(countValue))
      countValue = "0";
    return MeasureHelper.ConvertToMeasuredValue($"{countValue} шт", true);
  }

  private string ConvertToString(WeldingSeamGeometryType geometryType)
  {
    switch (geometryType)
    {
      case WeldingSeamGeometryType.WJT_Undefined:
        return "Не определено";
      case WeldingSeamGeometryType.WJT_Butt:
        return "Стыковое";
      case WeldingSeamGeometryType.WJT_Tauri:
        return "Тавровое";
      case WeldingSeamGeometryType.WJT_Corner:
        return "Угловое";
      case WeldingSeamGeometryType.WJT_Lap:
        return "Нахлесточное";
      default:
        return "Не поддерживается";
    }
  }

  private string ConvertToString(WeldingSeamSegmentationType segmentationType)
  {
    switch (segmentationType)
    {
      case WeldingSeamSegmentationType.ssSolid:
        return "Сплошной";
      case WeldingSeamSegmentationType.ssSegStep:
        return "Длина/шаг";
      case WeldingSeamSegmentationType.ssSegSpace:
        return "Сегмент/пропуск";
      case WeldingSeamSegmentationType.ssCountSeg:
        return "Количество/сегмент";
      case WeldingSeamSegmentationType.ssCountSpace:
        return "Количество/пропуск";
      case WeldingSeamSegmentationType.ssCountFillpercent:
        return "Количество/процент заполнения";
      case WeldingSeamSegmentationType.ssSegStepChess:
        return "Не поддерживается";
      default:
        return "Не поддерживается";
    }
  }

  private List<WeldingSeamExternalData> ReadWeldingSeamsFromDocumentFile()
  {
    string documentFilePath = this.fileVaultService.PublishTree(this.documentId, true, this.versionsRule, (IFileArea) this.fileVaultService.WorkArea);
    CadmechWeldingSeamsReader weldingSeamsReader = new CadmechWeldingSeamsReader();
    List<WeldingSeamExternalData> seamExternalDataList = weldingSeamsReader.Read(this.documentId, documentFilePath, this.integrator);
    if (weldingSeamsReader.IsUIFocusLost)
      ForegroundWindowHelper.Default.TrySetWindow(this.mainFormService.MainForm.Handle);
    foreach (WeldingSeamExternalData weldingSeam in seamExternalDataList)
      this.AdjustWeldingSeamExternalData(weldingSeam);
    return seamExternalDataList;
  }

  private void AdjustWeldingSeamExternalData(WeldingSeamExternalData weldingSeam)
  {
    weldingSeam.Number = this.ConvertToEmptyString(weldingSeam.Number);
    weldingSeam.StandardName = this.ConvertToEmptyString(weldingSeam.StandardName);
    weldingSeam.DesignationByStandard = this.ConvertToEmptyString(weldingSeam.DesignationByStandard);
    weldingSeam.WeldingMethodDesignationByStandard = this.ConvertToEmptyString(weldingSeam.WeldingMethodDesignationByStandard);
    weldingSeam.LegSizeByStandard = this.ConvertToEmptyString(weldingSeam.LegSizeByStandard);
    weldingSeam.LegUpperTolerance = this.ConvertToEmptyString(weldingSeam.LegUpperTolerance);
    weldingSeam.LegLowerTolerance = this.ConvertToEmptyString(weldingSeam.LegLowerTolerance);
    weldingSeam.ExtraDimensions = this.ConvertToEmptyString(weldingSeam.ExtraDimensions);
    weldingSeam.Note = this.ConvertToEmptyString(weldingSeam.Note);
    weldingSeam.Length = this.ConvertToEmptyString(weldingSeam.Length);
    weldingSeam.Count = this.ConvertToEmptyString(weldingSeam.Count);
    weldingSeam.ControlComplexDesignation = this.ConvertToEmptyString(weldingSeam.ControlComplexDesignation);
    weldingSeam.FullLength = this.ConvertToEmptyString(weldingSeam.FullLength);
    weldingSeam.LeftOffset = this.ConvertToEmptyString(weldingSeam.LeftOffset);
    weldingSeam.RightOffset = this.ConvertToEmptyString(weldingSeam.RightOffset);
    weldingSeam.SegmentStep = this.ConvertToEmptyString(weldingSeam.SegmentStep);
    weldingSeam.SegmentLength = this.ConvertToEmptyString(weldingSeam.SegmentLength);
    weldingSeam.Gap = this.ConvertToEmptyString(weldingSeam.Gap);
    weldingSeam.FirstPartThickness = this.ConvertToEmptyString(weldingSeam.FirstPartThickness);
    weldingSeam.SecondPartThickness = this.ConvertToEmptyString(weldingSeam.SecondPartThickness);
    weldingSeam.ConnectionKind = this.ConvertToEmptyString(weldingSeam.ConnectionKind);
    if (!string.IsNullOrEmpty(weldingSeam.Number) && (weldingSeam.Number.StartsWith("№") || weldingSeam.Number.StartsWith("#")))
      weldingSeam.Number = weldingSeam.Number.Remove(0, 1).Trim();
    if (weldingSeam.SegmentationType != WeldingSeamSegmentationType.ssSolid)
      return;
    weldingSeam.SegmentStep = string.Empty;
    weldingSeam.SegmentLength = string.Empty;
  }

  private string ConvertToEmptyString(string propertyValue)
  {
    return !string.IsNullOrEmpty(propertyValue) ? propertyValue : string.Empty;
  }

  private long? TryGetDocumentIdByPath(WeldingSeamComponent component)
  {
    return this.TryGetDocumentIdByPath(component.FilePath);
  }

  private long? TryGetDocumentIdByPath(string documentFilePath)
  {
    FileOrigin fileOrigin = this.fileVaultService.WorkArea.GetFileOrigin(documentFilePath, false);
    return fileOrigin.OriginType == FileOriginType.WorkFile ? new long?(fileOrigin.WorkObject.ObjectId) : new long?();
  }

  private class WeldingSeamMapping
  {
    public WeldingSeamMapping(
      WeldingSeamEntity weldingSeam,
      bool isNew,
      string externalKey,
      WeldingSeamExternalData externalData)
    {
      this.WeldingSeam = weldingSeam;
      this.IsNewSeam = isNew;
      this.ExternalKey = externalKey;
      this.ExternalData = externalData;
      this.ComponentMap = new List<UpdateWeldingSeamsAction.WeldingSeamComponentMapping>();
    }

    public WeldingSeamEntity WeldingSeam { get; private set; }

    public bool IsNewSeam { get; private set; }

    public string ExternalKey { get; private set; }

    public WeldingSeamExternalData ExternalData { get; private set; }

    public List<UpdateWeldingSeamsAction.WeldingSeamComponentMapping> ComponentMap { get; private set; }
  }

  private class WeldingSeamComponentMapping
  {
    public WeldingSeamComponentMapping(
      WeldingSeamComponent component,
      MechanicalArticleEntity article)
    {
      this.Component = component;
      this.Article = article;
    }

    public WeldingSeamComponent Component { get; private set; }

    public MechanicalArticleEntity Article { get; private set; }
  }
}
