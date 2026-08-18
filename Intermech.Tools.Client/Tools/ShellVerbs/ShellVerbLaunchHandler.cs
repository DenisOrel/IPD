// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.ShellVerbs.ShellVerbLaunchHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.LaunchActions;
using Intermech.Tools.Settings;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Tools.ShellVerbs;

internal sealed class ShellVerbLaunchHandler : ILaunchHandler, ILaunchHandlerFileEvents
{
  private readonly IFileVault fileVault;
  private readonly ShellVerbSettingsValidator validator;
  private readonly ShellVerbSettingsCodec codec;

  public ShellVerbLaunchHandler()
  {
    this.fileVault = ClientContext.FileVault;
    this.validator = new ShellVerbSettingsValidator();
    this.codec = new ShellVerbSettingsCodec();
  }

  public void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    ShellVerbSettings shellVerbSettings = handlerData != null ? (ShellVerbSettings) this.codec.Decode(handlerData) : throw new ArgumentNullException("handlerConfiguration");
    this.validator.Validate((ISettingsObject) shellVerbSettings, SettingsValidatorContext.Generic);
    if (string.IsNullOrEmpty(launchParams.ObjectFileName))
      launchParams.ObjectFileName = this.fileVault.DBFilesInfo.GetMasterFileName(launchParams.ObjectId, true);
    if (launchParams.FileArea == null)
      launchParams.FileArea = launchParams.LaunchType == LaunchType.Edit ? (IFileArea) this.fileVault.WorkArea : (IFileArea) this.fileVault.ViewArea;
    launchParams.ResultFilePath = this.fileVault.PublishTree(launchParams.ObjectId, launchParams.ObjectFileName, launchParams.VersionsRule, launchParams.FileArea);
    if (this.AfterPublishFile != null)
      this.AfterPublishFile((object) this, new LaunchHandlerEventArgs(launchParams));
    ProcessStartInfo startInfo = new ProcessStartInfo();
    startInfo.UseShellExecute = true;
    startInfo.FileName = launchParams.ResultFilePath;
    startInfo.Verb = shellVerbSettings.Verb;
    try
    {
      Process.Start(startInfo)?.Dispose();
    }
    catch (Win32Exception ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Tools.Client_182"), (object) launchParams.ObjectFileName);
      stringBuilder.Append(' ');
      stringBuilder.Append(ex.Message);
      throw new FaultException(stringBuilder.ToString(), (Exception) ex);
    }
  }

  public DataEditorControl CreateSettingsEditor()
  {
    return (DataEditorControl) new ShellVerbSettingsEditor();
  }

  public Guid Id => ShellVerbSettings.HandlerId;

  public string DisplayName => LocalizationHolder.rm.GetString("Tools.Client_184");

  public string GetServerObjectTemplate() => LocalizationHolder.rm.GetString("Tools.Client_183");

  public void BeforeLaunch(LaunchParams launchParams, XmlDocument handlerData)
  {
  }

  public event EventHandler<LaunchHandlerEventArgs> AfterPublishFile;
}
