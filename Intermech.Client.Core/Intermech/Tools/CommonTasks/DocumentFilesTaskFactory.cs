
// Type: Intermech.Tools.CommonTasks.DocumentFilesTaskFactory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.StandaloneView;
using System;


namespace Intermech.Tools.CommonTasks;

/// <summary>Фабрика задач спецобработки файлов документов.</summary>
public class DocumentFilesTaskFactory
{
  private IFileVault fileVaultService;
  private IOpenFilesService openFilesService;
  private IPrepareForViewDocumentFilesService prepareForViewService;
  private IStandaloneViewSettingsService standaloneViewSettingsService;
  private INotificationService notificationService;
  private IOutputView outputViewService;

  /// <summary>Создает объект.</summary>
  /// <param name="fileVaultService">Сервис файлового хранилища</param>
  /// <param name="openFilesService">Сервис файлов документов, открытых в приложении</param>
  /// <param name="prepareForViewService">Сервис подготовки локальных файлов документов к просмотру или печати</param>
  /// <param name="standaloneViewSettingsService">Сервис настроек автономного просмотра</param>
  /// <param name="notificationService">Сервис рассылки сообщений</param>
  /// <param name="outputViewService">Сервис окна вывода сообщений</param>
  /// <exception cref="T:System.ArgumentNullException">Один из параметров метода равен null</exception>
  public DocumentFilesTaskFactory(
    IFileVault fileVaultService,
    IOpenFilesService openFilesService,
    IPrepareForViewDocumentFilesService prepareForViewService,
    IStandaloneViewSettingsService standaloneViewSettingsService,
    INotificationService notificationService,
    IOutputView outputViewService)
  {
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (openFilesService == null)
      throw new ArgumentNullException(nameof (openFilesService));
    if (prepareForViewService == null)
      throw new ArgumentNullException(nameof (prepareForViewService));
    if (standaloneViewSettingsService == null)
      throw new ArgumentNullException(nameof (standaloneViewSettingsService));
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    if (outputViewService == null)
      throw new ArgumentNullException(nameof (outputViewService));
    this.fileVaultService = fileVaultService;
    this.openFilesService = openFilesService;
    this.prepareForViewService = prepareForViewService;
    this.standaloneViewSettingsService = standaloneViewSettingsService;
    this.notificationService = notificationService;
    this.outputViewService = outputViewService;
  }

  /// <summary>
  /// Создает задачу для внедрения в указанный файл документа информации для автономного просмотра.
  /// </summary>
  /// <returns>Созданная задача</returns>
  public InjectStandaloneViewDataTask InjectStandaloneViewData()
  {
    return new InjectStandaloneViewDataTask(this.standaloneViewSettingsService, this.outputViewService, this.prepareForViewService);
  }

  /// <summary>
  /// Создает задачу для создания аутентичного файла для указанного файла документа.
  /// </summary>
  /// <returns>Созданная задача</returns>
  public MakeAuthenticFileTask MakeAuthenticFile()
  {
    return new MakeAuthenticFileTask(this.fileVaultService, this.openFilesService, this.notificationService, new Func<InjectStandaloneViewDataTask>(this.InjectStandaloneViewData));
  }
}
