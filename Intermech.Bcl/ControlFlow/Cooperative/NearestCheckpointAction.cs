
// Type: Intermech.ControlFlow.Cooperative.NearestCheckpointAction
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.ControlFlow.Cooperative
{
    /// <summary>
    /// Позволяет реализовать обработчик для задач, когда требуется обработать группу объектов как единое целое, но общее количество
    /// обрабатываемых объектов изначально неизвестно. В этом и состоит отличие от обработчиков с синхронизацией по барьеру.
    /// Реализация использует механизм контрольных точек планировщика.
    /// </summary>
    public class NearestCheckpointAction : IAction
    {
      private readonly CooperativeScheduler scheduler;
      private readonly ManualResetEvent completeEvent;

      /// <summary>Создает объект задачи.</summary>
      /// <param name="scheduler">Планировщик</param>
      public NearestCheckpointAction(CooperativeScheduler scheduler)
      {
        this.scheduler = scheduler != null ? scheduler : throw new ArgumentNullException(nameof (scheduler));
        this.completeEvent = new ManualResetEvent(scheduler);
        scheduler.CreateImmediateCheckpoint().Wait((IAction) this);
      }

      /// <summary>Выполняет задачу.</summary>
      public void Perform()
      {
        this.DoPerform();
        this.completeEvent.Set();
      }

      /// <summary>Позволяет выполнить задачу.</summary>
      protected virtual void DoPerform()
      {
      }

      /// <summary>
      /// Объект синхронизации, устанавливаемый в активное состояние после выполнения задачи.
      /// </summary>
      public IWaitObject Complete => (IWaitObject) this.completeEvent;

      /// <summary>Планировщик задачи.</summary>
      protected CooperativeScheduler Scheduler => this.scheduler;
    }
}
