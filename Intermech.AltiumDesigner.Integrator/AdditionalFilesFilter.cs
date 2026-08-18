// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.AdditionalFilesFilter
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class AdditionalFilesFilter
{
  private readonly ADIntegratorSettings _settings;

  public string ProjectPath { get; }

  public AdditionalFilesFilter(string projectPath, ADIntegratorSettings settings)
  {
    this.ProjectPath = projectPath;
    this._settings = settings;
  }

  public IList<string> FilesExtensions
  {
    get
    {
      if (!string.IsNullOrEmpty(this._settings.AdditionalFilesExt))
      {
        string[] strArray = this._settings.AdditionalFilesExt.Split(',');
        if (strArray.Length != 0)
        {
          List<string> filesExtensions = new List<string>(strArray.Length);
          for (int index = 0; index < strArray.Length; ++index)
          {
            if (!string.IsNullOrEmpty(strArray[index]))
              filesExtensions.Add(!strArray[index].StartsWith(".") ? "." + strArray[index].ToLower() : strArray[index].ToLower());
          }
          return (IList<string>) filesExtensions;
        }
      }
      return (IList<string>) null;
    }
  }

  public bool InFilter(string file)
  {
    if (this._settings.NotImportingDir == null || this._settings.NotImportingDir.Count == 0)
      return false;
    foreach (string path2 in this._settings.NotImportingDir)
    {
      if (file.ToLower().StartsWith(Path.Combine(this.ProjectPath, path2).ToLower()))
        return true;
    }
    return false;
  }
}
