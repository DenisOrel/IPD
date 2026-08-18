// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.ExtApps.ExtAppLaunchHandler
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
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Tools.ExtApps;

internal sealed class ExtAppLaunchHandler : ILaunchHandler, ILaunchHandlerFileEvents
{
  private readonly IFileVault fileVault;
  private readonly ExtAppSettingsValidator validator;
  private readonly ExtAppSettingsCodec codec;

  public ExtAppLaunchHandler()
  {
    this.fileVault = ClientContext.FileVault;
    this.validator = new ExtAppSettingsValidator();
    this.codec = new ExtAppSettingsCodec();
  }

  public void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    ExtAppSettings settings = handlerData != null ? (ExtAppSettings) this.codec.Decode(handlerData) : throw new ArgumentNullException(nameof (handlerData));
    this.validator.Validate((ISettingsObject) settings, SettingsValidatorContext.Generic);
    if (string.IsNullOrEmpty(launchParams.ObjectFileName))
      launchParams.ObjectFileName = this.fileVault.DBFilesInfo.GetMasterFileName(launchParams.ObjectId, true);
    if (launchParams.FileArea == null)
      launchParams.FileArea = launchParams.LaunchType == LaunchType.Edit ? (IFileArea) this.fileVault.WorkArea : (IFileArea) this.fileVault.ViewArea;
    launchParams.ResultFilePath = this.fileVault.PublishTree(launchParams.ObjectId, launchParams.ObjectFileName, launchParams.VersionsRule, launchParams.FileArea);
    if (this.AfterPublishFile != null)
      this.AfterPublishFile((object) this, new LaunchHandlerEventArgs(launchParams));
    ProcessStartInfo processStartInfo = this.MakeStartInfo(settings, launchParams.ResultFilePath);
    this.ValidateStartInfo(settings, processStartInfo);
    try
    {
      Process.Start(processStartInfo).Dispose();
    }
    catch (Win32Exception ex)
    {
      throw new FaultException($"В процессе запуска приложения '{settings.ApplicationName}' произошла ошибка. Исполняемый файл приложения: {processStartInfo.FileName}. Код ошибки Win32: {ex.NativeErrorCode}. Сообщение Win32: {ex.Message}", (Exception) ex);
    }
  }

  private ProcessStartInfo MakeStartInfo(ExtAppSettings settings, string masterFile)
  {
    ProcessStartInfo processStartInfo = new ProcessStartInfo();
    processStartInfo.FileName = Environment.ExpandEnvironmentVariables(settings.Executable);
    string str1 = Environment.ExpandEnvironmentVariables(settings.Arguments);
    FileInfo fileInfo = new FileInfo(masterFile);
    string str2 = str1.Replace("!.!", fileInfo.FullName);
    int length1 = fileInfo.FullName.LastIndexOf(fileInfo.Extension);
    string str3 = str2.Replace("!", fileInfo.FullName.Substring(0, length1)).Replace("?.?", fileInfo.Name);
    int length2 = fileInfo.Name.LastIndexOf(fileInfo.Extension);
    processStartInfo.Arguments = str3.Replace("?", fileInfo.Name.Substring(0, length2));
    processStartInfo.WorkingDirectory = settings.WorkDirectory != string.Empty ? Environment.ExpandEnvironmentVariables(settings.WorkDirectory) : Path.GetDirectoryName(masterFile);
    processStartInfo.WindowStyle = settings.WindowStyle;
    processStartInfo.UseShellExecute = false;
    return processStartInfo;
  }

  private void ValidateStartInfo(ExtAppSettings settings, ProcessStartInfo processStartInfo)
  {
    if (!Directory.Exists(processStartInfo.WorkingDirectory))
      throw new FaultException($"Не удалось запустить приложение '{settings.ApplicationName}'. Рабочий каталог '{processStartInfo.WorkingDirectory}' не найден на диске. Проверьте настройки команды запуска приложения в окне 'Настройка\\Настройка инструментов'.");
    if (!File.Exists(processStartInfo.FileName))
      throw new FaultException($"Не удалось запустить приложение '{settings.ApplicationName}'. Исполняемый файл '{processStartInfo.FileName}' не найден на диске. Проверьте настройки команды запуска приложения в окне 'Настройка\\Настройка инструментов'.");
  }

  public DataEditorControl CreateSettingsEditor() => (DataEditorControl) new ExtAppActionsEditor();

  public Guid Id => ExtAppSettings.HandlerId;

  public string DisplayName => LocalizationHolder.rm.GetString("Tools.Client_119");

  public string GetServerObjectTemplate() => LocalizationHolder.rm.GetString("Tools.Client_118");

  public void BeforeLaunch(LaunchParams launchParams, XmlDocument handlerData)
  {
  }

  public event EventHandler<LaunchHandlerEventArgs> AfterPublishFile;
}
