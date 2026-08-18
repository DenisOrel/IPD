// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.InputParams
// Assembly: Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EB4A0A0B-E62B-4D21-A944-3B5D877E45CE
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.exe

using Intermech.Interfaces;
using Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App.Resources;

#nullable disable
namespace Intermech.XmlExchange.IpsXml.Converter.TechcardToIps.App;

internal sealed class InputParams : IAssignable
{
  public string UserName { get; set; }

  public string UserPassword { get; set; }

  public string UserRole { get; set; } = LocalizationHolder.rm.GetString("msgUserDefaultRoleName");

  public string ConfigFile { get; set; }

  public string InputFile { get; set; }

  public string WorkDir { get; set; }

  public bool? ShowProgress { get; set; }

  public string RolsynScriptCompiler { get; set; }

  public void Clear()
  {
    this.UserName = string.Empty;
    this.UserPassword = string.Empty;
    this.UserRole = string.Empty;
    this.ConfigFile = string.Empty;
    this.InputFile = string.Empty;
    this.WorkDir = string.Empty;
    this.ShowProgress = new bool?(false);
    this.RolsynScriptCompiler = string.Empty;
  }

  public void Assign(object source)
  {
    if (!(source is InputParams inputParams))
      return;
    this.UserName = inputParams.UserName;
    this.UserPassword = inputParams.UserPassword;
    this.UserRole = inputParams.UserRole;
    this.ConfigFile = inputParams.ConfigFile;
    this.InputFile = inputParams.InputFile;
    this.WorkDir = inputParams.WorkDir;
    this.ShowProgress = inputParams.ShowProgress;
    this.RolsynScriptCompiler = inputParams.RolsynScriptCompiler;
  }

  public void AssignNotEmpty(InputParams sourceParams)
  {
    if (string.IsNullOrEmpty(this.UserName))
      this.UserName = sourceParams.UserName;
    if (string.IsNullOrEmpty(this.UserPassword))
      this.UserPassword = sourceParams.UserPassword;
    if (string.IsNullOrEmpty(this.UserRole))
      this.UserRole = sourceParams.UserRole;
    if (string.IsNullOrEmpty(this.ConfigFile))
      this.ConfigFile = sourceParams.ConfigFile;
    if (string.IsNullOrEmpty(this.InputFile))
      this.InputFile = sourceParams.InputFile;
    if (string.IsNullOrEmpty(this.WorkDir))
      this.WorkDir = sourceParams.WorkDir;
    if (string.IsNullOrEmpty(this.RolsynScriptCompiler))
      this.RolsynScriptCompiler = sourceParams.RolsynScriptCompiler;
    if (this.ShowProgress.HasValue)
      return;
    this.ShowProgress = sourceParams.ShowProgress;
  }
}
