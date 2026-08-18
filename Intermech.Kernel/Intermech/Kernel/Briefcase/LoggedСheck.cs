// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.LoggedСheck
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Briefcase;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal abstract class LoggedСheck : ILogged<CheckMetadataLogItem>
{
  protected List<CheckMetadataLogItem> infoLog = new List<CheckMetadataLogItem>();
  protected string categoryCaption = string.Empty;
  protected bool isErrorAlways;

  public string UniIdentifiler { get; protected set; }

  public LoggedСheck(bool isErrorAlways) => this.isErrorAlways = isErrorAlways;

  public LoggedСheck(int category, bool isErrorAlways)
    : this(isErrorAlways)
  {
    this.categoryCaption = Consts.GetCategoryName(category);
    if (!(this.categoryCaption == string.Empty))
      return;
    this.categoryCaption = Consts.GetCategoryName(0);
  }

  public LoggedСheck(string categoryCaption, bool isErrorAlways)
    : this(isErrorAlways)
  {
    this.categoryCaption = categoryCaption;
  }

  protected void AddErrorToLog(string message, string briefValue, string dbValue)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.Error, message, briefValue, dbValue);
  }

  protected void AddErrorToLog(string message, string equalValue)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.Error, message, equalValue, equalValue);
  }

  protected void AddErrorToLog(string message)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.Error, message);
  }

  protected void AddInfoToLog(string message, string briefValue, string dbValue)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.Information, message, briefValue, dbValue);
  }

  protected void AddWarningToLog(string message, string briefValue, string dbValue)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.Warning, message, briefValue, dbValue);
  }

  protected void AddWarningToLog(string message)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.Warning, message);
  }

  protected void AddWarningLostDataToLog(string message, string briefValue, string dbValue)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.WarningLostData, message, briefValue, dbValue);
  }

  protected void AddWarningSystemToLog(string message, string briefValue, string dbValue)
  {
    this.AddInfoInLog(CheckMetadataLogItemType.WarningSystem, message, briefValue, dbValue);
  }

  protected void AddInfoInLog(
    CheckMetadataLogItemType cmlit,
    string message,
    string briefValue,
    string dbValue)
  {
    this.infoLog.Add(new CheckMetadataLogItem(cmlit, this.categoryCaption, this.UniIdentifiler, message, briefValue, dbValue));
  }

  protected void AddInfoInLog(CheckMetadataLogItemType cmlit, string message)
  {
    this.infoLog.Add(new CheckMetadataLogItem(cmlit, this.categoryCaption, this.UniIdentifiler, message));
  }

  public List<CheckMetadataLogItem> Log => this.infoLog;
}
