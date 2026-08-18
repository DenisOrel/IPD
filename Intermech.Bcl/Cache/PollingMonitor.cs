
// Type: Intermech.Cache.PollingMonitor
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading;


namespace Intermech.Cache
{
    /// <summary>
    /// Реализует монитор кэша, предназначенный для периодической проверки
    /// устаревания элементов кэша. Фоновый поток монитора обновляет объекты,
    /// контролирующие устаревание элементов, которые поддерживают интерфейс
    /// IPolledExpiration.
    /// </summary>
    public class PollingMonitor : IMonitor
    {
      /// <summary>
      /// Продолжительность интервала времени между периодическими обновлениями
      /// объектов, контролирующих устаревание элементов кэша.
      /// </summary>
      private TimeSpan interval;
      /// <summary>
      /// Коллекция объектов, контролирующих устаревание элементов кэша.
      /// </summary>
      private IDictionary expirations;
      /// <summary>Временный список объектов.</summary>
      private ArrayList tempList;

      /// <summary>Создает монитор кэша.</summary>
      /// <param name="interval">
      /// Интервал в миллисекундах между периодическими обновлениями
      /// объектов, контролирующих устаревание элементов кэша
      /// </param>
      public PollingMonitor(int interval)
        : this(new TimeSpan(0, 0, 0, 0, interval))
      {
      }

      /// <summary>Создает монитора кэша.</summary>
      /// <param name="interval">
      /// Интервал между периодическими обновлениями
      /// объектов, контролирующих устаревание элементов кэша
      /// </param>
      public PollingMonitor(TimeSpan interval)
      {
        this.interval = interval;
        this.expirations = (IDictionary) new HybridDictionary();
        this.tempList = new ArrayList(8);
        new Thread(new ThreadStart(this.Worker))
        {
          IsBackground = true,
          Priority = ThreadPriority.BelowNormal
        }.Start();
      }

      /// <summary>
      /// Добавляет в список контролиремых монитором элементов новый элемент.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      /// <param name="expirations">Массив объектов, с помощью которых кэш определяет устаревание элементов</param>
      public void Add(object key, IExpiration[] expirations)
      {
        Validator.CheckKey(key);
        Validator.CheckExpirations(expirations);
        if (expirations == null)
          return;
        lock (this.tempList)
        {
          for (int index = 0; index < expirations.Length; ++index)
          {
            if (expirations[index] is IPolledExpiration)
              this.tempList.Add((object) expirations[index]);
          }
          if (this.tempList.Count <= 0)
            return;
          lock (this.expirations)
            this.expirations[key] = (object) (IPolledExpiration[]) this.tempList.ToArray(typeof (IPolledExpiration));
          this.tempList.Clear();
        }
      }

      /// <summary>
      /// Удалает из списка контролируемых монитором элементов указанный элемент.
      /// </summary>
      /// <param name="key">Уникальный ключ элемента в кэше</param>
      public void Remove(object key)
      {
        Validator.CheckKey(key);
        lock (this.expirations)
          this.expirations.Remove(key);
      }

      /// <summary>Очищает список контролируемых монитором элементов.</summary>
      public void Flush()
      {
        lock (this.expirations)
          this.expirations.Clear();
      }

      /// <summary>
      /// Метод фонового потока монитора, который выполняет обновление
      /// объектов, контролирующих устаревание элементов кэша.
      /// </summary>
      private void Worker()
      {
        while (true)
        {
          try
          {
            Thread.Sleep(this.interval);
            this.PollExpirations();
          }
          catch (ThreadAbortException ex)
          {
            break;
          }
          catch
          {
          }
        }
      }

      /// <summary>
      /// Выполняет обновление контролирующих устаревание объектов.
      /// </summary>
      private void PollExpirations()
      {
        lock (this.expirations)
        {
          foreach (DictionaryEntry expiration in this.expirations)
          {
            foreach (IPolledExpiration polledExpiration in (IPolledExpiration[]) expiration.Value)
              polledExpiration.CheckExpired();
          }
        }
      }
    }
}
