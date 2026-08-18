
// Type: Intermech.Interfaces.IUserSessionAllocator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Позволяет реализовать механизм выделения сессий подключения к серверу приложений.
    /// </summary>
    public interface IUserSessionAllocator
    {
      /// <summary>Выделяет сессию подключения к серверу приложений.</summary>
      /// <returns>Дескриптор выделенной сессии</returns>
      IUserSessionDescriptor Allocate();

      /// <summary>Освобождает выделенную ранее сессию.</summary>
      /// <param name="descriptor">Дескриптор выделенной сессии</param>
      /// <exception cref="T:System.ArgumentNullException">Ссылка на дескриптор сессии не может быть null</exception>
      void Release(IUserSessionDescriptor descriptor);

      /// <summary>
      /// Завершает использование механизма выделения пользовательских сессий. Метод вызывается в конце работы приложения для
      /// корректного завершения приложения. Обычно, реализация этого метода используется для очистки пула сессий, если таковой имеется.
      /// </summary>
      void Shutdown();
    }
}
