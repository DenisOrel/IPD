// Decompiled with JetBrains decompiler
// Type: Intermech.Services.IMViewer.IMViewerLaunchHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.StandaloneView;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Services.IMViewer;

/// <summary>Обработчик команд запуска IMViewer.</summary>
internal sealed class IMViewerLaunchHandler : ParameterlessLaunchHandler, IDynamicLaunchHandler
{
  private readonly IFileVault fileVaultService;
  private readonly IIMViewerClientService imviewerService;
  private static readonly Guid HandlerId = new Guid("724C86CC-2846-4B76-85EB-3A79BD5BE760");
  private static readonly string HandlerName = "Открыть в IMViewer";

  public IMViewerLaunchHandler(IFileVault fileVaultService, IIMViewerClientService imviewerService)
    : base(IMViewerLaunchHandler.HandlerId, IMViewerLaunchHandler.HandlerName)
  {
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (imviewerService == null)
      throw new ArgumentNullException(nameof (imviewerService));
    this.fileVaultService = fileVaultService;
    this.imviewerService = imviewerService;
  }

  /// <summary>Выполняет поиск обработчика.</summary>
  /// <param name="dynamicLaunchInfo">Параметры поиска динамически подключаемого обработчика</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="dynamicLaunchInfo" /> содержит null</exception>
  public void Lookup(IDynamicLaunchInfo dynamicLaunchInfo)
  {
    if (dynamicLaunchInfo == null)
      throw new ArgumentNullException(nameof (dynamicLaunchInfo));
    if (dynamicLaunchInfo.LaunchParams.LaunchType != LaunchType.View || dynamicLaunchInfo.Handler != null || StandaloneViewVars.IsActive.Value || !this.imviewerService.Settings.EnableIntegration || !this.imviewerService.HasViewerObject(dynamicLaunchInfo.LaunchParams.ObjectId, dynamicLaunchInfo.LaunchParams.ObjectTypeId))
      return;
    dynamicLaunchInfo.Handler = (ILaunchHandler) this;
  }

  /// <summary>
  /// Выполняет извлечение на локальный диск файлов IMViewer и передачу их приложению IMViewer
  /// с помощью ShellExecure.
  /// </summary>
  /// <param name="launchParams">Описатель параметров запуска приложения</param>
  /// <param name="handlerData">Конфигурация для запускаемого приложения</param>
  public override void Launch(LaunchParams launchParams, XmlDocument handlerData)
  {
    if (launchParams == null)
      throw new ArgumentNullException(nameof (launchParams));
    List<IMViewerPublishItem> dataForOpenFiles = this.imviewerService.GetViewerDataForOpenFiles(launchParams.ObjectId, launchParams.ObjectTypeId, launchParams.VersionsRule);
    DBObjectState sourceDocument = dataForOpenFiles[0].SourceDocument;
    if (!this.CanViewInCurrentState(sourceDocument, dataForOpenFiles))
      return;
    CollectionUtils.RemoveAll<IMViewerPublishItem>((IList<IMViewerPublishItem>) dataForOpenFiles, (Predicate<IMViewerPublishItem>) (x => x.SidecarObject == null));
    PublishedObject publishedObject = this.fileVaultService.ViewArea.Publish((IList<DBObjectState>) CollectionUtils.ConvertAsList<IMViewerPublishItem, DBObjectState>((ICollection<IMViewerPublishItem>) dataForOpenFiles, (Converter<IMViewerPublishItem, DBObjectState>) (x => x.SidecarObject)));
    if (publishedObject.MasterFile == null)
      return;
    this.LaunchByIMViewer(sourceDocument, publishedObject.MasterFile.FullName);
  }

  private bool CanViewInCurrentState(
    DBObjectState sourceDocument,
    List<IMViewerPublishItem> imvPublishData)
  {
    if (this.IsPublishDataNotPresentOrNotSet(imvPublishData[0]))
    {
      int num = (int) MessageBox.Show($"Файлы '{sourceDocument.Caption}', необходимые для открытия в IMViewer, отсутствуют в базе данных. Просмотр невозможен.", IMViewerLaunchHandler.HandlerName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      return false;
    }
    return !CollectionUtils.Exists<IMViewerPublishItem>((IEnumerable<IMViewerPublishItem>) imvPublishData, new Predicate<IMViewerPublishItem>(this.IsPublishDataNotPresentOrNotActual)) || MessageBox.Show($"Файлы '{sourceDocument.Caption}', необходимые для открытия в IMViewer, содержат неактуальные или отсутствующие элементы. Вы хотите продолжить просмотр как есть?", IMViewerLaunchHandler.HandlerName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes;
  }

  private bool IsPublishDataNotPresentOrNotSet(IMViewerPublishItem publishItem)
  {
    return publishItem.SidecarObject == null || publishItem.SidecarContentStatus == ObjectContentStatus.NotSet;
  }

  private bool IsPublishDataNotPresentOrNotActual(IMViewerPublishItem publishItem)
  {
    return publishItem.SidecarObject == null || publishItem.SidecarContentStatus != ObjectContentStatus.Actual;
  }

  private void LaunchByIMViewer(DBObjectState sourceDocument, string imvFilePath)
  {
    ProcessStartInfo startInfo = new ProcessStartInfo();
    startInfo.UseShellExecute = true;
    startInfo.FileName = imvFilePath;
    if (startInfo.Verbs.Length == 0)
      throw new FaultException($"Не удалось открыть '{sourceDocument.Caption}' с помощью IMViewer. Возможно, приложение не установлено или не назначено в операционной системе, как приложение по умолчанию для IMV-файлов.");
    startInfo.Verb = startInfo.Verbs[0];
    try
    {
      Process.Start(startInfo)?.Dispose();
    }
    catch (Win32Exception ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("При открытии '{0}' с помощью IMViewer произошла ошибка.", (object) sourceDocument.Caption);
      stringBuilder.Append(' ');
      stringBuilder.Append(ex.Message);
      throw new FaultException(stringBuilder.ToString(), (Exception) ex);
    }
  }
}
