
// Type: Intermech.Files.SaveAllOnExitModule
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;


namespace Intermech.Files;

internal sealed class SaveAllOnExitModule : InitializerModule
{
  private INotificationService notificationService;
  private IFileVault fileVault;

  public SaveAllOnExitModule(INotificationService notificationService, IFileVault fileVault)
  {
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    if (fileVault == null)
      throw new ArgumentNullException(nameof (fileVault));
    this.notificationService = notificationService;
    this.fileVault = fileVault;
  }

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.notificationService.Subscribe("ApplicationClosing", new NotificationEventHandler(this.ApplicationClosingHandler));
  }

  /// <summary>
  /// Завершает работу объектов и сервисов, предоставленных модулем.
  /// Если свойство модуля IsInitialized возвращает false, то DoShutdown вызван как реакция на необработанное исключение при инициализации модуля.
  /// </summary>
  protected override void DoShutdown()
  {
    this.notificationService.Unsubscribe("ApplicationClosing", new NotificationEventHandler(this.ApplicationClosingHandler));
    base.DoShutdown();
  }

  private void ApplicationClosingHandler(object sender, NotificationEventArgs e)
  {
    this.UploadAllChangedFiles();
    this.FlushLocalState();
  }

  /// <summary>
  /// Записывает на диск те части внутреннего состояния файловых областей, которые сохраняются между сеансами работы приложения.
  /// </summary>
  private void FlushLocalState()
  {
    if (!(this.fileVault.WorkArea is IFileAreaLocalState workArea))
      return;
    workArea.Flush();
  }

  private void UploadAllChangedFiles()
  {
    List<DBObjectState> all = this.fileVault.WorkArea.GetPublishedObjects().FindAll((Predicate<DBObjectState>) (workObject => workObject.IsEditableState));
    this.fileVault.DBObjectsInfo.RemoveDeadObjects(all);
    DBObjectFilesDifferenceCalculator differenceCalculator = this.fileVault.WorkArea.CreateObjectFilesDifferenceCalculator(all.Count);
    differenceCalculator.AddRange((ICollection<DBObjectState>) all);
    differenceCalculator.Calculate();
    List<DBObjectFilesDifferences> unsavedObjects = this.fileVault.DBObjectsInfo.FindUnsavedObjects(differenceCalculator.Results, true);
    if (unsavedObjects.Count <= 0)
      return;
    List<DBObjectFilesDifferences> objectList = new List<DBObjectFilesDifferences>();
    using (IUploadProgressForm uploadProgressForm = (IUploadProgressForm) new LazyUploadProgressForm())
    {
      double percentComplete = 0.0;
      double num = 100.0 / (double) unsavedObjects.Count;
      long tickCount = (long) Environment.TickCount;
      for (int index = 0; index < unsavedObjects.Count; ++index)
      {
        uploadProgressForm.MakeVisible(tickCount);
        uploadProgressForm.ShowWorkObject(unsavedObjects[index].ObjectState);
        objectList.Clear();
        objectList.Add(unsavedObjects[index]);
        this.fileVault.WorkArea.Save(objectList);
        percentComplete += num;
        uploadProgressForm.ShowProgress(percentComplete);
        uploadProgressForm.DoEvents();
        if (uploadProgressForm.IsCancelRequested())
        {
          uploadProgressForm.ShowProgress(100.0);
          break;
        }
      }
    }
  }
}
