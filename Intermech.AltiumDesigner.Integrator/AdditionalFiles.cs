// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.AdditionalFiles
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class AdditionalFiles
{
  public static List<string> GetProjectAdditionalFiles(
    FileTypeService fileTypeSvc,
    IADProject project,
    string masterFile,
    ADIntegratorSettings settings)
  {
    List<string> first = new List<string>();
    foreach (DocumentInfo document in project.GetDocuments(true))
    {
      if (!fileTypeSvc.IsApplicationFile(document.FullPath))
        first.Add(document.FullPath);
    }
    FileTypeService fileTypeSvc1 = fileTypeSvc;
    IADProject project1 = project;
    string gerberFiles1 = settings.GerberFiles;
    string[] filesMasks;
    if (gerberFiles1 == null)
      filesMasks = (string[]) null;
    else
      filesMasks = gerberFiles1.Split(',');
    List<string> gerberFiles2 = AdditionalFiles.GetGerberFiles(fileTypeSvc1, project1, filesMasks, false);
    if (gerberFiles2.Count > 0)
      first.AddRange((IEnumerable<string>) gerberFiles2);
    List<string> additionalFiles = AdditionalFiles.GetAdditionalFiles(fileTypeSvc, new AdditionalFilesFilter(Path.GetDirectoryName(masterFile), settings));
    if (additionalFiles != null && additionalFiles.Count > 0)
      first = first.Union<string>((IEnumerable<string>) additionalFiles).ToList<string>();
    return first;
  }

  public static List<string> GetGerberFiles(
    FileTypeService fileTypeSvc,
    IADProject project,
    string[] filesMasks,
    bool includedInPCB)
  {
    List<string> gerberFiles = new List<string>();
    foreach (DocumentInfo generatedDocument in project.GeneratedDocuments)
    {
      if (!fileTypeSvc.IsApplicationFile(generatedDocument.FullPath))
        gerberFiles.Add(generatedDocument.FullPath);
    }
    if (filesMasks == null || filesMasks.Length == 0 || filesMasks.Length == 1 && string.IsNullOrEmpty(filesMasks[0]))
      return gerberFiles;
    Regex[] masks = Array.ConvertAll<string, Regex>(filesMasks, (Converter<string, Regex>) (a => RegexHelper.ToRegex(a, true)));
    return gerberFiles.FindAll((Predicate<string>) (a =>
    {
      bool flag = false;
      foreach (Regex regex in masks)
      {
        if (regex.IsMatch(a))
        {
          flag = true;
          break;
        }
      }
      if (includedInPCB & flag)
        return true;
      return !includedInPCB && !flag;
    }));
  }

  private static List<string> GetAdditionalFiles(
    FileTypeService fileTypeSvc,
    AdditionalFilesFilter filter)
  {
    IList<string> filesExtensions = filter.FilesExtensions;
    if (filesExtensions == null)
      return (List<string>) null;
    List<string> additionalFiles = new List<string>();
    foreach (string str in (IEnumerable<string>) filesExtensions)
    {
      foreach (string file in Directory.GetFiles(filter.ProjectPath, "*" + str, SearchOption.AllDirectories))
      {
        if (!fileTypeSvc.IsApplicationFile(file) && !filter.InFilter(file))
          additionalFiles.Add(file);
      }
    }
    return additionalFiles;
  }
}
