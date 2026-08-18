
// Type: Intermech.Interfaces.SingleSessionAllocator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Threading;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует механизм выделения сессий для SessionKeeper, когда в приложении используется только одна сессия подключения к серверу приложений.
    /// </summary>
    public sealed class SingleSessionAllocator : IUserSessionAllocator
    {
      private readonly SingleSessionAllocator.Descriptor mainSessionDescriptor;

      /// <summary>Создает объект.</summary>
      /// <param name="mainSession">Единственная сессия, которая будет использоваться приложением</param>
      /// <exception cref="T:System.ArgumentNullException">Ссылка на сессию не может быть null</exception>
      public SingleSessionAllocator(IUserSession mainSession)
      {
        this.mainSessionDescriptor = mainSession != null ? new SingleSessionAllocator.Descriptor(mainSession) : throw new ArgumentNullException(nameof (mainSession));
      }

      /// <summary>Выделяет сессию подключения к серверу приложений.</summary>
      /// <returns>Дескриптор выделенной сессии</returns>
      public IUserSessionDescriptor Allocate()
      {
        return Monitor.TryEnter((object) this.mainSessionDescriptor, 60000) ? (IUserSessionDescriptor) this.mainSessionDescriptor : throw new TimeoutException("Невозможно выделить сессию, так как она используется другим потоком.");
      }

      /// <summary>Освобождает выделенную ранее сессию.</summary>
      /// <param name="descriptor">Дескриптор выделенной сессии</param>
      /// <exception cref="T:System.ArgumentNullException">Ссылка на дескриптор сессии не может быть null</exception>
      public void Release(IUserSessionDescriptor descriptor)
      {
        SingleSessionAllocator.Descriptor descriptor1 = descriptor != null ? (SingleSessionAllocator.Descriptor) descriptor : throw new ArgumentNullException(nameof (descriptor));
        if (descriptor1 != this.mainSessionDescriptor)
          return;
        Monitor.Exit((object) descriptor1);
      }

      /// <summary>
      /// Завершает использование механизма выделения пользовательских сессий. Метод вызывается в конце работы приложения для
      /// корректного завершения приложения. Обычно, реализация этого метода используется для очистки пула сессий, если таковой имеется.
      /// </summary>
      public void Shutdown()
      {
      }

      private sealed class Descriptor : IUserSessionDescriptor
      {
        private readonly IUserSession session;

        public Descriptor(IUserSession session) => this.session = session;

        public IUserSession Session => this.session;

        /// <summary>
        /// Возвращает признак переиспользования сессии при вложенном создании SessionKeeper.
        /// Если создание SessionKeeper не вложено в область действия другого SessionKeeper,
        /// то значение свойства будет равно true, во всех остальных случаях - false.
        /// </summary>
        public bool IsTopmost => true;

        /// <summary>Изменяет режим освобождения сессии.</summary>
        /// <param name="newReleaseMode">Режим освобождения сессии</param>
        /// <returns>Признак успешного/неуспешного изменения режима</returns>
        public bool TrySetReleaseMode(UserSessionReleaseMode newReleaseMode)
        {
          return newReleaseMode == UserSessionReleaseMode.Normal;
        }
      }
    }
}
