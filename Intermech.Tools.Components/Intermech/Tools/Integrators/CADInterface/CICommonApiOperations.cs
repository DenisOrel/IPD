// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CICommonApiOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.IO;
using Intermech.Text;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class CICommonApiOperations
{
  private CICaptureChangesDriver driver;
  private IFileVault fileVault;

  internal CICommonApiOperations(CICaptureChangesDriver driver, IFileVault fileVault)
  {
    if (driver == null)
      throw new ArgumentNullException(nameof (driver));
    if (fileVault == null)
      throw new ArgumentNullException(nameof (fileVault));
    this.driver = driver;
    this.fileVault = fileVault;
  }

  public CICaptureChangesDriver CIDriver
  {
    [DebuggerStepThrough] get => this.driver;
  }

  public IFileVault FileVault
  {
    [DebuggerStepThrough] get => this.fileVault;
  }

  public List<AssemblyStructureRecord> GetArticleStructureCached(SectionEntity projectModelItem)
  {
    CIArticleStructureCache sectionObject = projectModelItem != null ? projectModelItem.Sections.Get<CIArticleStructureCache>((CIArticleStructureCache) null) : throw new ArgumentNullException(nameof (projectModelItem));
    if (sectionObject == null)
    {
      sectionObject = new CIArticleStructureCache(new AssemblyStructureManagerProxy(this.CIDriver.CADSystem));
      projectModelItem.Sections.Set((object) sectionObject);
    }
    if (sectionObject.Structure == null)
    {
      CADDocumentProxy document = projectModelItem.Sections.Get<CIDocumentData>().Document;
      sectionObject.Structure = PDMHelper.IsDocumentWithArticles(ObjectSection.GetObjectType(projectModelItem)) ? sectionObject.StructureManager.GetStructure(document, this.CIDriver.CanSynchronizeSubstitutions(projectModelItem)) : new List<AssemblyStructureRecord>(0);
      foreach (AssemblyStructureRecord assemblyStructureRecord in sectionObject.Structure)
      {
        switch (CADDocumentHelper.ReadPDMFlag((IServiceProvider) this.CIDriver.Integrator, assemblyStructureRecord.ComponentConfiguration))
        {
          case 2:
          case 6:
            string replacementName = CADDocumentHelper.TryGetReplacementName((IServiceProvider) this.CIDriver.Integrator, assemblyStructureRecord.ComponentConfiguration);
            if (!string.IsNullOrEmpty(replacementName) && !PathUtils.IsSamePath(replacementName, (string) assemblyStructureRecord.ComponentConfiguration.Name))
            {
              ModelConfigurationProxy configuration = assemblyStructureRecord.ComponentConfiguration.Document.TryGetConfiguration(replacementName);
              if (configuration != null)
              {
                assemblyStructureRecord.ComponentConfiguration = configuration;
                continue;
              }
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      string workAreaPath = this.FileVault.WorkArea.AreaPath;
      sectionObject.Structure.RemoveAll((Predicate<AssemblyStructureRecord>) (record =>
      {
        int pdmFlag = CADDocumentHelper.ReadPDMFlag((IServiceProvider) this.CIDriver.Integrator, record.ComponentConfiguration);
        return !PathUtils.IsPlacedIn(record.ComponentMasterFile, workAreaPath) && pdmFlag != 1 || CADDocumentHelper.IsArticleCreationDenied(pdmFlag);
      }));
    }
    return sectionObject.Structure;
  }

  internal bool FlushArticleStructureChanges(SectionEntity projectDocumentItem)
  {
    CIArticleStructureCache articleStructureCache = projectDocumentItem != null ? projectDocumentItem.Sections.Get<CIArticleStructureCache>((CIArticleStructureCache) null) : throw new ArgumentNullException(nameof (projectDocumentItem));
    if (articleStructureCache == null || articleStructureCache.Structure == null)
      return false;
    articleStructureCache.StructureManager.CommitChanges();
    return true;
  }

  public string MakeArticleDisplayName(
    string configurationName,
    string configurationFilePath,
    string documentMasterFilePath)
  {
    if (string.IsNullOrEmpty(configurationName))
      throw new ArgumentException("Не задано имя конфигурации документа.", nameof (configurationName));
    string strB = !string.IsNullOrEmpty(documentMasterFilePath) ? TextServices.Trim(Path.GetFileName(string.IsNullOrEmpty(configurationFilePath) ? documentMasterFilePath : configurationFilePath)) : throw new ArgumentException("Не задан путь к мастер-файлу документа.", nameof (documentMasterFilePath));
    string strA = TextServices.Trim(configurationName);
    return string.Compare(strA, strB, true) != 0 ? $"{strA} (файл {strB})" : strA;
  }

  public string MakeArticleKey(string configurationName, string documentMasterFilePath)
  {
    if (string.IsNullOrEmpty(configurationName))
      throw new ArgumentException("Не задано имя конфигурации документа.", nameof (configurationName));
    if (string.IsNullOrEmpty(documentMasterFilePath))
      throw new ArgumentException("Не задан путь к мастер-файлу документа.", nameof (documentMasterFilePath));
    if (!Path.IsPathRooted(documentMasterFilePath))
      throw new ArgumentException("Путь к мастер-файлу документа должен быть задан в абсолютной форме.", nameof (documentMasterFilePath));
    return $"{TextServices.Trim(configurationName)} [{TextServices.Trim(documentMasterFilePath)}]";
  }
}
