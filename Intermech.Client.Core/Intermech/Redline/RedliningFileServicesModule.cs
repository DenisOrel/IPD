
// Type: Intermech.Redline.RedliningFileServicesModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Files;
using System;


namespace Intermech.Redline;

/// <summary>
/// Модуль сервисов и обработчиков, обеспечивающих корректную обработку ядром IPS файлов красного карандаша.
/// </summary>
internal sealed class RedliningFileServicesModule : InitializerModule
{
  private IFileVault fileVaultService;

  public RedliningFileServicesModule(IFileVault fileVaultService)
  {
    this.fileVaultService = fileVaultService != null ? fileVaultService : throw new ArgumentNullException(nameof (fileVaultService));
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.InstallFileVaultExtensions();
  }

  /// <summary>
  /// Завершает работу объектов и сервисов, предоставленных модулем.
  /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
  /// </summary>
  protected override void DoShutdown()
  {
    base.DoShutdown();
    this.RemoveFileVaultExtensions();
  }

  private void InstallFileVaultExtensions()
  {
  }

  private void RemoveFileVaultExtensions()
  {
  }

  private void CanControlReadOnlyFileAttribute(object sender, CanControlFileAttributeEventArgs e)
  {
    int num = e.CanControl ? 1 : 0;
  }
}
