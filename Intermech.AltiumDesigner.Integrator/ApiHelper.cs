// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ApiHelper
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal static class ApiHelper
{
  public static List<string> GetAdditionalSheets(IADProject project, string documentName)
  {
    List<string> additionalSheets = new List<string>();
    foreach (DocumentInfo document in project.GetDocuments(true))
    {
      if (document is SchemaDocumentInfo)
      {
        SchemaDocumentInfo schemaDocumentInfo = document as SchemaDocumentInfo;
        switch ((int) Array.Find<Parameter>(schemaDocumentInfo.ObligatoryParameters, (Predicate<Parameter>) (component => component.Name == "SheetNumber")).Value)
        {
          case -1:
          case 1:
            continue;
          default:
            Parameter parameter = Array.Find<Parameter>(schemaDocumentInfo.ObligatoryParameters, (Predicate<Parameter>) (element => element.Name == "DocumentNumber"));
            if (parameter.Equals((object) (string) parameter.Value))
            {
              additionalSheets.Add(document.FullPath);
              continue;
            }
            continue;
        }
      }
    }
    return additionalSheets;
  }

  private static Exception CreateFileNotFoundException(string fileName, Exception exception)
  {
    return new Exception($"Файл {fileName} не найден!", exception);
  }

  public static void OpenObject(IIPSAddIn addIn, string fileName)
  {
    try
    {
      addIn.OpenObject(fileName);
    }
    catch (FileNotFoundException ex)
    {
      throw ApiHelper.CreateFileNotFoundException(fileName, (Exception) ex);
    }
  }

  public static ISchDocument GetSchDocument(IIPSAddIn addIn, string fileName, bool open)
  {
    try
    {
      return addIn.GetSchDocument(fileName, open);
    }
    catch (FileNotFoundException ex)
    {
      throw ApiHelper.CreateFileNotFoundException(fileName, (Exception) ex);
    }
  }

  public static IPCBDocument GetPCBDocument(IIPSAddIn addIn, string fileName)
  {
    try
    {
      return addIn.GetPCBDocument(fileName);
    }
    catch (FileNotFoundException ex)
    {
      throw ApiHelper.CreateFileNotFoundException(fileName, (Exception) ex);
    }
  }

  public static IADProject GetProject(IIPSAddIn addIn, string fileName)
  {
    try
    {
      return addIn.GetProject(fileName);
    }
    catch (FileNotFoundException ex)
    {
      throw ApiHelper.CreateFileNotFoundException(fileName, (Exception) ex);
    }
  }
}
