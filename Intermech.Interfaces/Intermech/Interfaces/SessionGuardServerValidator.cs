
// Type: Intermech.Interfaces.SessionGuardServerValidator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый класс для серверных объектов, реализующий защиту объектов сервера приложений от использования вне SessionKeeper.
    /// </summary>
    public class SessionGuardServerValidator
    {
      /// <summary>
      /// Проверяет, выполняется ли обращение к сессии или сессионному объекту из блока SessionKeeper. Если это не так, то метод сбрасывает исключение.
      /// </summary>
      /// <param name="userSession">Используемая сессия</param>
      /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
      /// <exception cref="T:Intermech.Interfaces.SessionGuardException">Использование объектов сервера приложений вне SessionKeeper строжайше запрещено</exception>
      protected void ValidateCall(IUserSession userSession)
      {
        if (!this.CanValidateCall(userSession))
          return;
        this.DoValidateCall(userSession);
      }

      /// <summary>
      /// Проверяет, следует ли защищать обращение к сессии или сессионному объекту от использования вне SessionKeeper.
      /// </summary>
      /// <param name="userSession">Используемая сессия</param>
      /// <returns>true, если защиту следует использовать</returns>
      /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
      protected virtual bool CanValidateCall(IUserSession userSession)
      {
        return userSession.IsSessionGuardActive && !SessionGuardContext.IsSuspended();
      }

      /// <summary>
      /// Проверяет, выполняется ли обращение к сессии или сессионному объекту из блока SessionKeeper. Если это не так, то метод сбрасывает исключение.
      /// </summary>
      /// <param name="userSession">Используемая сессия</param>
      /// <exception cref="T:System.ArgumentNullException">Аргумент метода не может быть null</exception>
      /// <exception cref="T:Intermech.Interfaces.SessionGuardException">Использование объектов сервера приложений вне SessionKeeper строжайше запрещено</exception>
      private void DoValidateCall(IUserSession userSession)
      {
        Guid activeClientSession = SessionGuardContext.GetActiveClientSession();
        if (activeClientSession != Guid.Empty && activeClientSession != userSession.SessionGUID)
          throw new SessionGuardException();
      }
    }
}
