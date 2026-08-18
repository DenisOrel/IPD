// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.DocumentHelper
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal static class DocumentHelper
{
  public static DocumentFileData ReadDocumentData(string masterFilePath, AddInProxy proxy)
  {
    if (masterFilePath == null)
      throw new ArgumentNullException(nameof (masterFilePath));
    if (proxy == null)
      throw new ArgumentNullException(nameof (proxy));
    DocumentFileData documentFileData = new DocumentFileData(masterFilePath);
    documentFileData.CustomSections.Set((object) proxy);
    return documentFileData;
  }

  public static List<ADDocument> GetProjectDocuments(
    List<DocumentInfo> projectDocuments,
    FileTypeService fileTypeSvc,
    AddInProxy proxy)
  {
    Dictionary<string, string> dictionary1 = new Dictionary<string, string>(projectDocuments.Count);
    Dictionary<string, List<string>> dictionary2 = new Dictionary<string, List<string>>(projectDocuments.Count);
    List<string> collection = new List<string>();
    List<ADDocument> projectDocuments1 = new List<ADDocument>();
    foreach (DocumentInfo projectDocument in projectDocuments)
    {
      if (fileTypeSvc.IsApplicationFile(projectDocument.FullPath))
      {
        if (projectDocument is SchemaDocumentInfo)
        {
          SchemaDocumentInfo schemaDocumentInfo = projectDocument as SchemaDocumentInfo;
          int num = (int) Array.Find<Parameter>(schemaDocumentInfo.ObligatoryParameters, (Predicate<Parameter>) (component => component.Name == "SheetNumber")).Value;
          string key = (string) Array.Find<Parameter>(schemaDocumentInfo.ObligatoryParameters, (Predicate<Parameter>) (component => component.Name == "DocumentNumber")).Value;
          if (num == 1 || num == -1)
          {
            if (dictionary1.ContainsKey(key))
              throw new Exception($"Первые листы различных схем должны иметь различные значения параметра {"DocumentNumber"}");
            dictionary1.Add(key, schemaDocumentInfo.FullPath);
          }
          else
          {
            List<string> stringList;
            if (!dictionary2.TryGetValue(key, out stringList))
              collection.Add(schemaDocumentInfo.FullPath);
            else
              stringList.Add(schemaDocumentInfo.FullPath);
          }
        }
        else
          projectDocuments1.Add(new ADDocument(proxy, projectDocument.FullPath));
      }
    }
    foreach (KeyValuePair<string, string> keyValuePair in dictionary1)
    {
      List<string> stringList;
      if (!dictionary2.TryGetValue(keyValuePair.Key, out stringList))
        stringList = new List<string>(0);
      if (collection.Count > 0)
      {
        stringList.AddRange((IEnumerable<string>) collection);
        collection.Clear();
      }
      ADDocument adDocument = new ADDocument(proxy, keyValuePair.Value);
      if (stringList.Count > 0)
        adDocument.AdditionalDocuments = new List<ADDocument>(stringList.Count);
      foreach (string fullPath in stringList)
        adDocument.AdditionalDocuments.Add(new ADDocument(proxy, fullPath));
      projectDocuments1.Add(adDocument);
    }
    return projectDocuments1;
  }

  public static List<BoardData<ADDocument>> GetProjectBoards(
    List<DocumentInfo> projectDocuments,
    FileTypeService fileTypeSvc,
    AddInProxy proxy,
    ADIntegratorSettings settings)
  {
    List<ADDocument> all = DocumentHelper.GetProjectDocuments(projectDocuments, fileTypeSvc, proxy).FindAll((Predicate<ADDocument>) (x => x.DocumentType == ADDocumentType.SCH));
    Dictionary<string, ADDocument> projectItems = new Dictionary<string, ADDocument>(all.Count);
    foreach (ADDocument adDocument in all)
      projectItems.Add(adDocument.FullPath, adDocument);
    return ProjectBoardsReader.Read(settings, projectItems);
  }

  public static Guid GetElectricalSchemaType(string designation)
  {
    string suffix = ElectricalTypesHelper.GetSuffix(designation);
    return string.IsNullOrEmpty(suffix) ? Guid.Empty : DocumentHelper.GetElectricalSchemaTypeFromSuffix(suffix);
  }

  private static Guid GetElectricalSchemaTypeFromSuffix(string suffix)
  {
    switch (suffix.ToUpper())
    {
      case "Э0":
        return AltiumObjectTypeGuids.ElectricCircuitE0;
      case "Э1":
        return AltiumObjectTypeGuids.ElectricCircuitE1;
      case "Э2":
        return AltiumObjectTypeGuids.ElectricCircuitE2;
      case "Э3":
        return AltiumObjectTypeGuids.ElectricCircuitE3;
      case "Э4":
        return AltiumObjectTypeGuids.ElectricCircuitE4;
      case "Э5":
        return AltiumObjectTypeGuids.ElectricCircuitE5;
      case "Э6":
        return AltiumObjectTypeGuids.ElectricCircuitE6;
      case "Э7":
        return AltiumObjectTypeGuids.ElectricCircuitE7;
      default:
        return Guid.Empty;
    }
  }
}
