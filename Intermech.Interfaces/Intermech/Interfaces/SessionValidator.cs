
// Type: Intermech.Interfaces.SessionValidator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый класс валидатора состояния для пользовательских сессий сервера приложений.
    /// Валидатор вызывается автоматически при извлечении сессий из пула и возврате их в пул,
    /// оп позволяет защититься от ошибок программиста - незакрытых транзакций и т.п.
    /// </summary>
    public abstract class SessionValidator
    {
      /// <summary>
      /// Реализует проверку состояния пользовательской сессии. Если состояние сессии невалидно, то метод
      /// делает запись об этом в журнале системы, предпринимает попытку исправить состояние сессии и
      /// возвращает результат проверки в виде специального объекта. Метод не бросает исключений,
      /// вместо этого он помещает исключение в объект с результатом вызова в свойство <see cref="P:ErrorException" />.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Результат проверки состояния сессии</returns>
      /// <exception cref="T:ArgumentNullException">session</exception>
      public SessionValidatorResult Validate(IUserSession session)
      {
        if (session == null)
          throw new ArgumentNullException(nameof (session));
        try
        {
          return this.DoValidate(session);
        }
        catch (Exception ex)
        {
          return new SessionValidatorResult(new Exception($"An unhandled exception detected in a session validator '{this}'.", ex));
        }
      }

      /// <summary>
      /// Реализует проверку состояния пользовательской сессии. Если состояние сессии невалидно, то метод должен
      /// сделать запись об этом в журнале системы, предпринять попытку исправить состояние сессии и
      /// вернуть результат проверки в виде специального объекта. Метод не должен бросать исключений,
      /// если о невалидном состоянии сессии необходимо сообщить с помощью исключения, то
      /// объект исключения должен быть помещен в объект с результатом вызова в свойство <see cref="P:ErrorException" />.
      /// </summary>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Результат проверки состояния сессии</returns>
      protected virtual SessionValidatorResult DoValidate(IUserSession session)
      {
        return SessionValidatorResult.Success;
      }
    }
}
