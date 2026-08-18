
// Type: Intermech.Runtime.ComInterop.LocalServer.LiveComObjectsTracker
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Трекер используемых COM-объектов приложения. Реализация является thread safe.
    /// </summary>
    internal sealed class LiveComObjectsTracker
    {
      private object syncRoot;
      private IReferenceCounter processRefCounter;
      private bool isActive;
      private List<WeakReference> comObjectList;
      private int comObjectListCleanupPeriod;
      private Timer comObjectListCleanupTimer;

      /// <summary>Создает объект.</summary>
      /// <param name="processRefCounter">Счетчик ссылок для процесса приложения</param>
      /// <param name="cleanupPeriod">Интервал периодической очистки списка используемых COM-объектов приложения</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="processRefCounter" /> не должен быть равен null</exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">Параметр <paramref name="cleanupPeriod" /> слишком мал</exception>
      public LiveComObjectsTracker(IReferenceCounter processRefCounter, TimeSpan cleanupPeriod)
      {
        if (processRefCounter == null)
          throw new ArgumentNullException(nameof (processRefCounter));
        int totalMilliseconds = (int) cleanupPeriod.TotalMilliseconds;
        if (totalMilliseconds == 0)
          throw new ArgumentOutOfRangeException(nameof (cleanupPeriod));
        this.syncRoot = new object();
        this.processRefCounter = processRefCounter;
        this.comObjectListCleanupPeriod = totalMilliseconds;
      }

      /// <summary>Возвращает true, если трекер был активирован.</summary>
      public bool IsActive
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.isActive;
        }
      }

      /// <summary>Активирует трекер, если это еще не было сделано.</summary>
      public void EnsureActive()
      {
        lock (this.syncRoot)
        {
          if (this.IsActive)
            return;
          this.Activate();
        }
      }

      /// <summary>Активирует трекер.</summary>
      /// <exception cref="T:System.InvalidOperationException">Трекер используемых COM-объектов уже был активирован</exception>
      public void Activate()
      {
        lock (this.syncRoot)
        {
          if (this.isActive)
            throw new InvalidOperationException(ComServerResources.SR_LiveComObjectsTrackerIsAlreadyActive);
          this.comObjectList = new List<WeakReference>();
          this.isActive = true;
        }
      }

      /// <summary>
      /// Добавляет COM-объект в список отслеживаемых COM-объектов.
      /// </summary>
      /// <param name="comObject">COM-объект</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comObject" /> не должен быть равен null</exception>
      /// <exception cref="T:System.InvalidOperationException">Трекер используемых COM-объектов не был активирован</exception>
      public void AddObject(object comObject)
      {
        if (comObject == null)
          throw new ArgumentNullException(nameof (comObject));
        lock (this.syncRoot)
        {
          if (!this.isActive)
            throw new InvalidOperationException(ComServerResources.SR_LiveComObjectsTrackerIsNotActive);
          int count = this.comObjectList.Count;
          this.comObjectList.Add(new WeakReference(comObject));
          if (TraceSwitches.General.TraceInfo)
            Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_LiveComObjectsCountChanged, (object) this.comObjectList.Count));
          this.processRefCounter.Increment();
          if (count != 0 || this.comObjectListCleanupTimer != null)
            return;
          this.comObjectListCleanupTimer = new Timer(new TimerCallback(this.RemoveUnusedComObjectsTask), (object) null, 1000, this.comObjectListCleanupPeriod);
        }
      }

      private void RemoveUnusedComObjectsTask(object arg)
      {
        lock (this.syncRoot)
        {
          int count = this.comObjectList.Count;
          int num = this.comObjectList.RemoveAll(new Predicate<WeakReference>(this.IsUnusedComObjectReference));
          if (num == 0)
            return;
          if (TraceSwitches.General.TraceInfo)
            Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_LiveComObjectsCountChanged, (object) this.comObjectList.Count));
          this.processRefCounter.Decrement(num);
        }
      }

      private bool IsUnusedComObjectReference(WeakReference comObjectRef)
      {
        object target = comObjectRef.Target;
        return target == null || this.GetComObjectRefCount(target) == 0;
      }

      private int GetComObjectRefCount(object comObject)
      {
        return Marshal.Release(Marshal.GetIUnknownForObject(comObject));
      }
    }
}
