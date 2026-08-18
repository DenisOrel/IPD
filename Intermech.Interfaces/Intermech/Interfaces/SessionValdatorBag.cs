
// Type: Intermech.Interfaces.SessionValdatorBag
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует коллекцию валидаторов пользовательских сессий сервера приложений, которая позволяет потокобезопасно добавлять
    /// новые валидаторы и использовать существующие валидаторы без использования блокировки потокой.
    /// Реализация коллекции является thread safe.
    /// </summary>
    public class SessionValdatorBag
    {
      private int writeSeq;
      private List<Func<SessionValidator>> createFunctions;
      private ThreadLocal<ICollection<SessionValidator>> threadBoundValidators;
      private ThreadLocal<long> threadBoundWriteSeq;
      private static readonly SessionValidator[] emptyValidators = new SessionValidator[0];

      /// <summary>Создает объект.</summary>
      public SessionValdatorBag()
      {
        this.writeSeq = 0;
        this.createFunctions = new List<Func<SessionValidator>>();
        this.threadBoundValidators = new ThreadLocal<ICollection<SessionValidator>>();
        this.threadBoundValidators.Value = (ICollection<SessionValidator>) SessionValdatorBag.emptyValidators;
        this.threadBoundWriteSeq = new ThreadLocal<long>();
        this.threadBoundWriteSeq.Value = (long) this.writeSeq;
      }

      /// <summary>
      /// Добавляет функцию создания экземпляра валидатора. Фукнция будет использована, когда вызывающему потоку потребуется коллекция валидаторов.
      /// </summary>
      /// <param name="createFunction">Функция создания валидатора</param>
      /// <exception cref="T:ArgumentNullException">createFunction</exception>
      public void Add(Func<SessionValidator> createFunction)
      {
        if (createFunction == null)
          throw new ArgumentNullException(nameof (createFunction));
        Interlocked.Increment(ref this.writeSeq);
        lock (this.createFunctions)
          this.createFunctions.Add(createFunction);
      }

      /// <summary>Удаляет функцию создания экземпляра валидатора.</summary>
      /// <param name="createFunction">Функция создания валидатора</param>
      /// <exception cref="T:ArgumentNullException">createFunction</exception>
      public void Remove(Func<SessionValidator> createFunction)
      {
        if (createFunction == null)
          throw new ArgumentNullException(nameof (createFunction));
        Interlocked.Increment(ref this.writeSeq);
        lock (this.createFunctions)
          this.createFunctions.Remove(createFunction);
      }

      /// <summary>
      /// Возвращает коллекцию валидаторов, которая может использоваться вызывающим потоком без каких-либо синхронизаций или блокировок.
      /// </summary>
      /// <returns>Коллекция валидаторов</returns>
      public ICollection<SessionValidator> GetValidators()
      {
        if (this.threadBoundValidators.Value != null && this.threadBoundWriteSeq.Value != (long) this.writeSeq)
          this.threadBoundValidators.Value = (ICollection<SessionValidator>) null;
        if (this.threadBoundValidators.Value == null)
        {
          this.threadBoundWriteSeq.Value = (long) this.writeSeq;
          this.threadBoundValidators.Value = this.CreateValidators();
        }
        return this.threadBoundValidators.Value;
      }

      private ICollection<SessionValidator> CreateValidators()
      {
        lock (this.createFunctions)
        {
          if (this.createFunctions.Count == 0)
            return (ICollection<SessionValidator>) SessionValdatorBag.emptyValidators;
          List<SessionValidator> validators = new List<SessionValidator>(this.createFunctions.Count);
          foreach (Func<SessionValidator> createFunction in this.createFunctions)
            validators.Add(createFunction());
          return (ICollection<SessionValidator>) validators;
        }
      }
    }
}
