
// Type: Intermech.Navigator.CompositionCopierTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using System;
using System.Threading;
using System.Windows;


namespace Intermech.Navigator;

/// <summary>
/// Задача копирования состава для выборки или классификатора из объекта-прототипа
/// </summary>
internal sealed class CompositionCopierTask : CustomBackgroundTask
{
  private Guid _copierGuid;

  public override void Stop()
  {
    ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ISelectionsService)) as ISelectionsService).StopCopyStructure(this._copierGuid);
  }

  /// <summary>
  /// Метод потока. Запускает на сервере задачу копирования и выводит результаты.
  /// </summary>
  /// <param name="args"></param>
  private void Create(object args)
  {
    try
    {
      CompositionCopierTaskArgs compositionCopierTaskArgs = (CompositionCopierTaskArgs) args;
      this._state = BackgroundTaskState.Running;
      this.OnChanged(BackgroundTaskChangedType.State);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._name = $"Создание состава для {sessionKeeper.Session.GetObject(compositionCopierTaskArgs.ObjectID).NameInMessages}";
        this.OnChanged(BackgroundTaskChangedType.Text);
        ISelectionsService customService = sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
        this._copierGuid = customService.StartCopyStructure((object) sessionKeeper.Session.SessionGUID, this._name, compositionCopierTaskArgs.TemplateObjectID, compositionCopierTaskArgs.ObjectID);
        try
        {
          StructureCopierStateInfo copyStructureInfo;
          for (copyStructureInfo = customService.GetCopyStructureInfo(this._copierGuid); copyStructureInfo != null && copyStructureInfo.State == OperationStates.Processing; copyStructureInfo = customService.GetCopyStructureInfo(this._copierGuid))
          {
            this._value = copyStructureInfo.CurrentUnit;
            this.OnChanged(BackgroundTaskChangedType.Value);
            Thread.Sleep(30);
          }
          this._state = BackgroundTaskState.Terminated;
          this.OnChanged(BackgroundTaskChangedType.State);
          if (copyStructureInfo == null)
            return;
          if (copyStructureInfo.State == OperationStates.Done)
          {
            this._value = 100;
            this.OnChanged(BackgroundTaskChangedType.Value);
            INotificationService service = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
            foreach (long createdObjectId in copyStructureInfo.CreatedObjectIDs)
              service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", createdObjectId));
            foreach (long createdRelationId in copyStructureInfo.CreatedRelationIDs)
              service.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("RelationsCreated", createdRelationId));
            int num = (int) MessageBox.Show($"Задача \"{this._name}\" успешно завершена!", "Копирование состава", MessageBoxButton.OK, MessageBoxImage.Asterisk);
          }
          else
          {
            if (copyStructureInfo.State != OperationStates.Error || copyStructureInfo.Exception == null)
              return;
            ExceptionHelper.ExceptionService.ShowException(new Exception($"Ошибка при выполнении задачи \"{this._name}\"", copyStructureInfo.Exception));
          }
        }
        finally
        {
          customService.StopCopyStructure(this._copierGuid);
          this.OnChanged(BackgroundTaskChangedType.Dispose);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  /// <summary>Запуск процесса копирования состава.</summary>
  /// <param name="objectID">Идентификатор версии объекта в составе которого создается копия структуры</param>
  /// <param name="templateObjectID">Идентификатор версии объекта-прототипа копируемой структуры</param>
  public static void BeginCreate(long objectID, long templateObjectID)
  {
    CompositionCopierTask task = new CompositionCopierTask();
    if (ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service)
      service.AddTask((IBackgroundTask) task);
    new Thread(new ParameterizedThreadStart(task.Create))
    {
      Name = $"ClassifierCompositionCopier_{Guid.NewGuid()}",
      IsBackground = true
    }.Start((object) new CompositionCopierTaskArgs(objectID, templateObjectID));
  }
}
