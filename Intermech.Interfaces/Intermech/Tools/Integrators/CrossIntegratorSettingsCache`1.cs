
// Type: Intermech.Tools.Integrators.CrossIntegratorSettingsCache`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Memoization;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Integrators
{
    /// <summary>
    /// Этот класс позволяет реализовать вычисление и кэширование значений, зависящих от настроек нескольких/всех интеграторов в системе.
    /// Реализация не является thread safe.
    /// </summary>
    /// <typeparam name="T">Тип вычисляемого значения. Он должен быть immutable или thread-safe</typeparam>
    public sealed class CrossIntegratorSettingsCache<T> where T : class
    {
      private TimeSpan serverCheckPeriod;
      private IStateMonitor resetMonitor;
      private Func<T> valueFactory;
      private object resetSeq;
      private DateTime lastServerCheck;
      private long valueWriteSeq;
      private T value;

      public CrossIntegratorSettingsCache(
        TimeSpan serverCheckPeriod,
        IStateMonitor resetMonitor,
        Func<T> valueFactory)
      {
        if (valueFactory == null)
          throw new ArgumentNullException(nameof (valueFactory));
        this.serverCheckPeriod = serverCheckPeriod;
        this.resetMonitor = resetMonitor;
        this.valueFactory = valueFactory;
        this.ResetInternal();
      }

      /// <summary>Создает объект.</summary>
      /// <param name="cacheManager">Менеджер кэша настроек интеграторов</param>
      /// <param name="valueFactory">Фабрика значения</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="cacheManager" />, <paramref name="valueFactory" /> не должны быть равны null</exception>
      public CrossIntegratorSettingsCache(
        IntegratorSettingsCacheManager cacheManager,
        Func<T> valueFactory)
      {
        if (cacheManager == null)
          throw new ArgumentNullException(nameof (cacheManager));
        if (valueFactory == null)
          throw new ArgumentNullException(nameof (valueFactory));
        this.serverCheckPeriod = cacheManager.ServerCheckPeriod;
        this.resetMonitor = cacheManager.ResetMonitor;
        this.valueFactory = valueFactory;
        this.ResetInternal();
      }

      /// <summary>Сбрасывает вычисленное значение.</summary>
      public void Reset() => this.ResetInternal();

      private void ResetInternal()
      {
        this.resetSeq = this.resetMonitor.WriterSeqNum;
        this.lastServerCheck = DateTime.MinValue;
        this.valueWriteSeq = -1L;
        this.value = default (T);
      }

      /// <summary>Возвращает вычисленное значение.</summary>
      public T Value
      {
        [DebuggerStepThrough] get
        {
          if (this.resetMonitor.AnyWritersSince(this.resetSeq) || DateTime.Now - this.lastServerCheck > this.serverCheckPeriod)
          {
            DateTime now = DateTime.Now;
            object writerSeqNum = this.resetMonitor.WriterSeqNum;
            long writeSeq;
            using (SessionKeeper sessionKeeper = new SessionKeeper())
              writeSeq = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true).WriteSeq;
            if (this.valueWriteSeq < writeSeq || (object) this.value == null)
            {
              this.value = this.valueFactory();
              this.valueWriteSeq = writeSeq;
            }
            this.resetSeq = writerSeqNum;
            this.lastServerCheck = now;
          }
          return this.value;
        }
      }
    }
}
