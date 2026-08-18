// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Files;
using Intermech.Tools.Integrators;
using Intermech.Tools.LaunchActions;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Реализует паттерн окружающий контекст для клиентов IPS (ambient context).
/// Класс предоставляет доступ доступ к глобальным, обязательно существующим сервисам клиента, которые используются в большом количестве клиентских классов.
/// Все свойства класса, используемые для доступа к экземплярам сервисов, никогда не возвращают null.
/// </summary>
public static class ClientContext
{
  private static ApplicationServiceRef<IOpenFilesService> openFilesRef = new ApplicationServiceRef<IOpenFilesService>();
  private static ApplicationServiceRef<IFileVault> fileVaultRef = new ApplicationServiceRef<IFileVault>();
  private static ApplicationServiceRef<IFileImportService> fileImporterRef = new ApplicationServiceRef<IFileImportService>();
  private static ApplicationServiceRef<IIntegratorRegistry> integratorsRef = new ApplicationServiceRef<IIntegratorRegistry>();
  private static ApplicationServiceRef<ILaunchActionService> launchActionsRef = new ApplicationServiceRef<ILaunchActionService>();

  /// <summary>Возвращает сервис открытых файлов.</summary>
  public static IOpenFilesService OpenFiles
  {
    [DebuggerStepThrough] get => ClientContext.openFilesRef.Value;
  }

  /// <summary>Возвращает сервис файлового хранилища пользователя.</summary>
  public static IFileVault FileVault
  {
    [DebuggerStepThrough] get => ClientContext.fileVaultRef.Value;
  }

  /// <summary>Возвращает сервис импорта файлов в IPS.</summary>
  public static IFileImportService FileImporter
  {
    [DebuggerStepThrough] get => ClientContext.fileImporterRef.Value;
  }

  /// <summary>
  /// Возвращает сервис, обслуживающий интеграторы с другими приложениями.
  /// </summary>
  public static IIntegratorRegistry Integrators
  {
    [DebuggerStepThrough] get => ClientContext.integratorsRef.Value;
  }

  /// <summary>
  /// Возвращает сервис, обслуживающий команды запуска приложений.
  /// </summary>
  public static ILaunchActionService LaunchActions
  {
    [DebuggerStepThrough] get => ClientContext.launchActionsRef.Value;
  }
}
