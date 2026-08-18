// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.TransferFileToWorkspaceAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Controls;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Settings;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators;

public class TransferFileToWorkspaceAction : IAction
{
  private static readonly object yesToAllKey = new object();
  private readonly IOpenFilesService openFilesService;
  private readonly IFileVaultSettingsService fileVaultSettingsService;
  private string sourcePath;
  private string targetPath;
  private TransferFileToWorkspaceMode _importMode;

  public TransferFileToWorkspaceAction(
    IOpenFilesService openFilesService,
    IFileVaultSettingsService fileVaultSettingsService)
  {
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    if (fileVaultSettingsService == null)
      throw new ArgumentNullException(nameof (fileVaultSettingsService));
    this.openFilesService = openFilesService;
    this.fileVaultSettingsService = fileVaultSettingsService;
  }

  public string SourcePath
  {
    get => this.sourcePath;
    set => this.sourcePath = value;
  }

  public string TargetPath => this.targetPath;

  /// <summary>Режим как нужно импортировать файлы</summary>
  public TransferFileToWorkspaceMode ImportMode
  {
    get => this._importMode;
    set => this._importMode = value;
  }

  public void Perform()
  {
    try
    {
      this.ValidateProperties();
      this.DoClearResultProperties();
      this.DoPerform();
    }
    catch
    {
      this.DoClearResultProperties();
      throw;
    }
    finally
    {
      this.DoCleanup();
    }
  }

  private void ValidateProperties()
  {
    if (string.IsNullOrEmpty(this.sourcePath))
      throw new InvalidOperationException("Не задано свойство SourcePath.");
  }

  private void DoPerform()
  {
    IFileVault service = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
    IFileArea area = service.FindArea(this.sourcePath);
    if (area != null && area == service.WorkArea)
      this.targetPath = this.sourcePath;
    else if (!this.CanTransfer())
    {
      this.targetPath = this.sourcePath;
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IFileNamesService customService = (IFileNamesService) sessionKeeper.Session.GetCustomService(typeof (IFileNamesService));
        this.targetPath = Path.Combine(service.WorkArea.AreaPath, customService.GetUniqueFileName(Path.GetFileName(this.sourcePath), -1L, sessionKeeper.Session.SessionGUID));
      }
      this.CheckTargetAndCopy(this.sourcePath, this.targetPath);
      if (this.ImportMode != TransferFileToWorkspaceMode.FilesByMask)
        return;
      string directoryName = Path.GetDirectoryName(this.sourcePath);
      string withoutExtension1 = Path.GetFileNameWithoutExtension(this.sourcePath);
      string withoutExtension2 = Path.GetFileNameWithoutExtension(this.targetPath);
      foreach (string str in ((IEnumerable<FileInfo>) new DirectoryInfo(directoryName).GetFiles(withoutExtension1 + ".*", SearchOption.TopDirectoryOnly)).Select<FileInfo, string>((Func<FileInfo, string>) (x => x.FullName)).Where<string>((Func<string, bool>) (fullName => fullName != this.sourcePath)))
      {
        string targetPath = Path.Combine(service.WorkArea.AreaPath, Path.GetFileName(str).Replace(withoutExtension1, withoutExtension2));
        this.CheckTargetAndCopy(str, targetPath);
      }
    }
  }

  /// <summary>
  /// Проверяем существует ли файл и копируем либо перемещаем его в зависимости от настроек
  /// </summary>
  /// <param name="sourcePath">Оригинальный путь</param>
  /// <param name="targetPath">Сформированный путь</param>
  private void CheckTargetAndCopy(string sourcePath, string targetPath)
  {
    if (File.Exists(targetPath))
    {
      if (this.openFilesService.IsOpen(targetPath))
        this.openFilesService.Unload((IEnumerable<string>) new string[1]
        {
          targetPath
        });
      FileUtils.DeleteFileSilently(targetPath);
    }
    if ((bool) (ValueCell<bool>) this.fileVaultSettingsService.CommonSettings.LeaveSourcesOfImportedFiles)
      File.Copy(sourcePath, targetPath);
    else
      File.Move(sourcePath, targetPath);
  }

  private bool CanTransfer()
  {
    object objA;
    if (UIVars.UICommand.Value != null && UIVars.UICommand.Value.Tags.TryGetValue(TransferFileToWorkspaceAction.yesToAllKey, out objA) && object.Equals(objA, (object) true))
      return true;
    List<IMMessageBoxButton> messageBoxButtonList = new List<IMMessageBoxButton>();
    if (UIVars.UICommand.Value != null)
      messageBoxButtonList.Add(new IMMessageBoxButton("Да, для всех", DialogResult.OK));
    messageBoxButtonList.Add(new IMMessageBoxButton("Да", DialogResult.Yes));
    messageBoxButtonList.Add(new IMMessageBoxButton("Нет", DialogResult.No));
    int num = (int) IMMessageBox.Show("Импорт файла", string.Format("Импортируемый файл '{0}' должен находиться в рабочей области файлового хранилища. {2} {1}", (object) this.sourcePath, (bool) (ValueCell<bool>) this.fileVaultSettingsService.CommonSettings.LeaveSourcesOfImportedFiles ? (object) "Скопировать его туда?" : (object) "Переместить его туда?", (bool) (ValueCell<bool>) this.fileVaultSettingsService.CommonSettings.LeaveSourcesOfImportedFiles ? (object) "Для составных документов будет скопирован только выбранный файл, а ссылочные зависимости скопированы не будут." : (object) "Для составных документов будет перемещен только выбранный файл, а ссылочные зависимости перемещены не будут."), messageBoxButtonList.ToArray(), IMMessageBoxImage.Question);
    if (num == 1)
      UIVars.UICommand.Value.Tags[TransferFileToWorkspaceAction.yesToAllKey] = (object) true;
    return num != 7;
  }

  private void DoClearResultProperties() => this.targetPath = (string) null;

  private void DoCleanup()
  {
  }
}
