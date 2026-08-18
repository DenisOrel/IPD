
// Type: Intermech.Interfaces.IUserSessionDescriptor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Позволяет реализовать дескриптор для сессии подключения к серверу приложений, выделенной с помощью SessionKeeper.
    /// Используется при реализации <see cref="T:Intermech.Interfaces.IUserSessionAllocator" />.
    /// </summary>
    public interface IUserSessionDescriptor
    {
      /// <summary>Выделенная пользовательская сессия.</summary>
      IUserSession Session { get; }

      /// <summary>
      /// Возвращает признак переиспользования сессии при вложенном создании SessionKeeper.
      /// Если создание SessionKeeper не вложено в область действия другого SessionKeeper,
      /// то значение свойства будет равно true, во всех остальных случаях - false.
      /// </summary>
      bool IsTopmost { get; }

      /// <summary>Изменяет режим освобождения сессии.</summary>
      /// <param name="newReleaseMode">Режим освобождения сессии</param>
      /// <returns>Признак успешного/неуспешного изменения режима</returns>
      bool TrySetReleaseMode(UserSessionReleaseMode newReleaseMode);
    }
}
