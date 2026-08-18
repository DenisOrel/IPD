// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgOperations
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.Cadmech.Integrator.DwgTasks;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal static class DwgOperations
{
  public static List<string> GetLiveXRefs(IIntegrator integrator, string dwgFilePath)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (string.IsNullOrEmpty(dwgFilePath))
      throw new ArgumentException();
    DwgOperations.SaveIfModified(integrator, dwgFilePath);
    List<string> xrefs;
    using (DwgReaderTask dwgReaderTask = new DwgReaderTask())
    {
      dwgReaderTask.OpenDrawing(dwgFilePath);
      xrefs = dwgReaderTask.GetXRefs();
    }
    ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    string directoryName = Path.GetDirectoryName(dwgFilePath);
    List<string> liveXrefs = new List<string>(xrefs.Count);
    foreach (string str1 in xrefs)
    {
      string str2 = Path.IsPathRooted(str1) ? str1 : Path.GetFullPath(Path.Combine(directoryName, str1));
      liveXrefs.Add(str2);
    }
    return liveXrefs;
  }

  public static void FilterLiveXRefs(string dwgFilePath, List<string> xrefs)
  {
    if (xrefs == null)
      throw new ArgumentNullException(nameof (xrefs));
    IFileVault fileVault = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    xrefs.RemoveAll((Predicate<string>) (xrefPath =>
    {
      if (!File.Exists(xrefPath))
        return true;
      if (PathUtils.IsPlacedIn(xrefPath, fileVault.WorkArea.AreaPath))
        return false;
      if (UIReport.Enabled)
        UIReport.ReportEvent($"Внешняя ссылка '{xrefPath}' из чертежа '{Path.GetFileName(dwgFilePath)}' проигнорирована, т.к. она расположена вне рабочей области пользователя в файловом хранилище.", TraceLevel.Warning);
      return true;
    }));
  }

  public static ContainerValues GetStamp(
    IIntegrator integrator,
    SectionEntity dwgDocumentItem,
    DrawingTypeSettings dwgTypeSettings)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (dwgDocumentItem == null)
      throw new ArgumentNullException();
    if (dwgTypeSettings == null)
      throw new ArgumentNullException();
    ValueBag bag;
    if (string.IsNullOrEmpty(dwgTypeSettings.StmName))
    {
      if (UIReport.Enabled)
        UIReport.ReportEvent($"Сканер чертежей: обработка чертежа '{FilesSection.GetMasterFile(dwgDocumentItem)}' проводится не будет, т.к. она отключена в настройках интегратора.");
      bag = DwgOperations.GetFakeStampValues(dwgDocumentItem);
    }
    else
    {
      string stmFilePath = StmFile.Locate(dwgTypeSettings);
      if (stmFilePath == null)
      {
        if (UIReport.Enabled)
          UIReport.ReportEvent($"Сканер чертежей: обработка чертежа '{FilesSection.GetMasterFile(dwgDocumentItem)}' не может быть выполнена, т.к. файл настроек '{dwgTypeSettings.StmName}' не был найден.", TraceLevel.Warning);
        bag = DwgOperations.GetFakeStampValues(dwgDocumentItem);
      }
      else
      {
        if (UIReport.Enabled)
          UIReport.ReportEvent($"Сканер чертежей: обработка чертежа '{FilesSection.GetMasterFile(dwgDocumentItem)}' будет выполнена с использованием файл настроек '{stmFilePath}'.");
        bag = DwgOperations.GetFirstNormalStamp(integrator, dwgDocumentItem, stmFilePath, (List<StringKey>) null, new Predicate<ValueBag>(DwgPredicates.StampIsValid));
      }
    }
    return new ContainerValues(bag, false);
  }

  public static ValueBag GetFirstNormalStamp(
    IIntegrator integrator,
    SectionEntity dwgDocumentItem,
    string stmFilePath,
    List<StringKey> attributes,
    Predicate<ValueBag> match)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (dwgDocumentItem == null)
      throw new ArgumentNullException();
    if (string.IsNullOrEmpty(stmFilePath))
      throw new ArgumentException();
    if (match == null)
      throw new ArgumentNullException(nameof (match));
    string masterFile = FilesSection.GetMasterFile(dwgDocumentItem);
    DwgOperations.CheckDwgExists(masterFile);
    DwgOperations.CheckStmExists(stmFilePath);
    DwgOperations.SaveIfModified(integrator, masterFile);
    using (DwgReaderTask dwgReaderTask = new DwgReaderTask())
    {
      dwgReaderTask.StmFilePath = stmFilePath;
      dwgReaderTask.OpenDrawing(masterFile);
      ValueBag firstNormalStamp = dwgReaderTask.SeekStamp(attributes, match);
      if (firstNormalStamp != null)
        return firstNormalStamp;
    }
    return DwgOperations.GetFakeStampValues(dwgDocumentItem, stmFilePath, attributes);
  }

  public static ValueBag GetFakeStampValues(
    SectionEntity dwgDocumentItem,
    string stmFilePath,
    List<StringKey> attributes)
  {
    if (dwgDocumentItem == null)
      throw new ArgumentNullException();
    if (string.IsNullOrEmpty(stmFilePath))
      throw new ArgumentException();
    DwgOperations.CheckStmExists(stmFilePath);
    AttributesSection attributesSection = dwgDocumentItem.Sections.Get<AttributesSection>((AttributesSection) null);
    if (attributesSection == null)
      return new ValueBag();
    ValueBag fakeStampValues = attributesSection.DatabaseSet.Copy();
    fakeStampValues.SetFlagForAll(NamedFlags.ReadOnly);
    if (attributes == null)
      attributes = StmFile.ReadFields(stmFilePath).ConvertAll<StringKey>((Converter<string, StringKey>) (name => new StringKey(name)));
    foreach (ValueRecord valueRecord in fakeStampValues.FindAll((Predicate<ValueRecord>) (item => !attributes.Contains(item.Key))))
      valueRecord.Remove();
    fakeStampValues.AcceptChanges();
    return fakeStampValues;
  }

  public static ValueBag GetFakeStampValues(SectionEntity dwgDocumentItem)
  {
    if (dwgDocumentItem == null)
      throw new ArgumentNullException();
    ValueBag fakeStampValues = dwgDocumentItem.Sections.Get<AttributesSection>().DatabaseSet.Copy();
    fakeStampValues.SetFlagForAll(NamedFlags.ReadOnly);
    return fakeStampValues;
  }

  private static void CheckDwgExists(string dwgFilePath)
  {
    if (!File.Exists(dwgFilePath))
      throw new FileNotFoundException($"Файл сканируемого чертежа '{dwgFilePath}' не найден на диске.", dwgFilePath);
  }

  private static void CheckStmExists(string stmFilePath)
  {
    if (!File.Exists(stmFilePath))
      throw new FileNotFoundException($"Файл '{stmFilePath}' с параметрами сканирования чертежа не найден на диске.", stmFilePath);
  }

  private static bool IsStmEmpty(string stmFilePath)
  {
    foreach (string readAllLine in File.ReadAllLines(stmFilePath))
    {
      if (!string.IsNullOrEmpty(readAllLine.Trim()))
        return false;
    }
    return true;
  }

  private static void SaveIfModified(IIntegrator integrator, string dwgPath)
  {
    CadApiService service = ServiceUtils.GetService<CadApiService>((object) integrator, true);
    if (!service.IsApplicationRunning)
      return;
    using (AcadApiSession acadApiSession = new AcadApiSession((IApplicationApiService) service))
    {
      ICadDocumentProxy openDocument = acadApiSession.Application.FindOpenDocument(dwgPath);
      if (openDocument == null || !openDocument.Modified)
        return;
      openDocument.Save();
    }
  }
}
