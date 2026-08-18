// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.SynchronizeOperations
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.UI;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal class SynchronizeOperations
{
  private IFileVault fileVaultService;

  public SynchronizeOperations(IFileVault fileVaultService)
  {
    this.fileVaultService = fileVaultService != null ? fileVaultService : throw new ArgumentNullException(nameof (fileVaultService));
  }

  public bool EnableUIReport { get; set; }

  protected IFileVault FileVaultService => this.fileVaultService;

  public PDMDocumentVersionInfo GetActualDocumentVersion(long id)
  {
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    long objectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectId = sessionKeeper.Session.GetObjectByVersionsRule(id, editorRule.OwnerId, true).ObjectID;
    DBObjectState objectState = this.fileVaultService.DBObjectsInfo.GetObjectState(objectId, true);
    string masterFileName = this.fileVaultService.DBFilesInfo.GetMasterFileName(objectId, true);
    return new PDMDocumentVersionInfo(id, objectState, masterFileName);
  }

  public List<DBObjectState> GetActualDocumentStructure(long id, bool versionMode = false)
  {
    return this.fileVaultService.DBObjectsInfo.CreateStateListForObjectTree(versionMode ? id : this.GetActualDocumentVersion(id).DBObjectState.ObjectId, VersionsRuleSources.GetEditorRule());
  }

  public PDMDocumentSynchronizationInfo AnalyzeDocumentStructure(
    PDMDocumentVersionInfo rootDocumentVersion,
    List<DBObjectState> documentStructure)
  {
    if (rootDocumentVersion == null)
      throw new ArgumentNullException(nameof (rootDocumentVersion));
    if (documentStructure == null)
      throw new ArgumentNullException(nameof (documentStructure));
    if (documentStructure.Count == 0 || rootDocumentVersion.DBObjectState.Id != documentStructure[0].Id)
      throw new ArgumentException("Головной документ должен быть первым элементом в структуре документа.", nameof (documentStructure));
    List<DBObjectState> dbObjectStateList = new List<DBObjectState>((IEnumerable<DBObjectState>) documentStructure);
    List<DBObjectState> unpublishedObjects = this.fileVaultService.DBObjectsInfo.ExtractUnpublishedObjects(dbObjectStateList, (IFileAreaPublishedObjects) this.fileVaultService.WorkArea);
    if (this.EnableUIReport)
      this.ReportProcessedObjects((ICollection<DBObjectState>) unpublishedObjects, LocalizationHolder.rm.GetString("SR_308"));
    this.SaveDocumentsToDisk(rootDocumentVersion, (ICollection<DBObjectState>) dbObjectStateList);
    DBObjectFilesDifferenceCalculator differenceCalculator = this.fileVaultService.WorkArea.CreateObjectFilesDifferenceCalculator(dbObjectStateList.Count);
    differenceCalculator.AddRange((ICollection<DBObjectState>) dbObjectStateList);
    differenceCalculator.Calculate();
    if (this.EnableUIReport)
      this.ReportDiffObjects((ICollection<DBObjectFilesDifferences>) differenceCalculator.Results, LocalizationHolder.rm.GetString("SR_309"));
    List<DBObjectState> objectStateList1 = SynchronizeOperations.ToObjectStateList((ICollection<DBObjectFilesDifferences>) this.fileVaultService.DBObjectsInfo.FindOutdatedObjects(differenceCalculator.Results, false));
    List<DBObjectState> unsavedWorkObjects = SynchronizeOperations.ToObjectStateList((ICollection<DBObjectFilesDifferences>) this.fileVaultService.DBObjectsInfo.FindUnsavedObjects(differenceCalculator.Results, false));
    List<DBObjectState> objectStateList2 = SynchronizeOperations.ToObjectStateList((ICollection<DBObjectFilesDifferences>) CollectionUtils.FindAllAsList<DBObjectFilesDifferences>((ICollection<DBObjectFilesDifferences>) differenceCalculator.Results, (Predicate<DBObjectFilesDifferences>) (diffItem => diffItem.ObjectState.IsEditableState && !unsavedWorkObjects.Contains(diffItem.ObjectState))));
    return new PDMDocumentSynchronizationInfo(unpublishedObjects, objectStateList1, unsavedWorkObjects, objectStateList2);
  }

  private static List<DBObjectState> ToObjectStateList(
    ICollection<DBObjectFilesDifferences> diffItems)
  {
    return CollectionUtils.ConvertAsList<DBObjectFilesDifferences, DBObjectState>(diffItems, (Converter<DBObjectFilesDifferences, DBObjectState>) (item => item.ObjectState));
  }

  protected virtual void SaveDocumentsToDisk(
    PDMDocumentVersionInfo rootDocumentVersion,
    ICollection<DBObjectState> documents)
  {
  }

  private void ReportProcessedObjects(ICollection<DBObjectState> objects, string eventCaption)
  {
    UIReport.ReportEvent(eventCaption);
    UIReport.Indent();
    UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_310"), (object) objects.Count));
    foreach (DBObjectState dbObjectState in (IEnumerable<DBObjectState>) objects)
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_311"), (object) dbObjectState.ObjectId, (object) dbObjectState.Caption));
    UIReport.Unindent();
  }

  private void ReportDiffObjects(
    ICollection<DBObjectFilesDifferences> diffResult,
    string eventCaption)
  {
    UIReport.ReportEvent(eventCaption);
    UIReport.Indent();
    UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_310"), (object) diffResult.Count));
    foreach (DBObjectFilesDifferences filesDifferences in (IEnumerable<DBObjectFilesDifferences>) diffResult)
    {
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_311"), (object) filesDifferences.ObjectState.ObjectId, (object) filesDifferences.ObjectState.Caption));
      UIReport.Indent();
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_310"), (object) filesDifferences.DifferencePairs.Count));
      foreach (FileDifferencePair differencePair in filesDifferences.DifferencePairs)
      {
        string str1 = differencePair.RemoteState != null ? differencePair.RemoteState.FileName : differencePair.LocalState.FileName;
        string str2 = differencePair.LocalState != null ? differencePair.LocalState.LastWriteTimeUtc.ToString() : LocalizationHolder.rm.GetString("SR_312");
        string str3 = differencePair.RemoteState != null ? differencePair.RemoteState.LastWriteTimeUtc.ToString() : LocalizationHolder.rm.GetString("SR_312");
        string localFileResolution = this.GetLocalFileResolution(differencePair.DifferenceType);
        UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("SR_313"), (object) str1, (object) str2, (object) str3, (object) localFileResolution));
      }
      UIReport.Unindent();
    }
    UIReport.Unindent();
  }

  private string GetLocalFileResolution(FileDifferenceType diffType)
  {
    switch (diffType)
    {
      case FileDifferenceType.MissingFile:
        return LocalizationHolder.rm.GetString("SR_314");
      case FileDifferenceType.OutdatedFile:
        return LocalizationHolder.rm.GetString("SR_315");
      case FileDifferenceType.UnchangedFile:
        return LocalizationHolder.rm.GetString("SR_316");
      case FileDifferenceType.UpdatedFile:
        return LocalizationHolder.rm.GetString("SR_317");
      case FileDifferenceType.NewFile:
        return LocalizationHolder.rm.GetString("SR_318");
      default:
        throw new NotSupportedEnumException((Enum) diffType);
    }
  }
}
