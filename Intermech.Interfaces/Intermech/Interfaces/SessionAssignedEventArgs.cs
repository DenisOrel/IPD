
// Type: Intermech.Interfaces.SessionAssignedEventArgs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Реализует класс аргументов для события выделения сессии какому-либо потоку кода.
    /// </summary>
    public sealed class SessionAssignedEventArgs : EventArgs
    {
      private IUserSession userSession;

      /// <summary>Создает объект.</summary>
      /// <param name="userSession">Пользовательская сессия</param>
      internal SessionAssignedEventArgs(IUserSession userSession) => this.userSession = userSession;

      /// <summary>
      /// Возвращает выделенную для потока пользовательскую сессию.
      /// </summary>
      public IUserSession UserSession => this.userSession;
    }
}
