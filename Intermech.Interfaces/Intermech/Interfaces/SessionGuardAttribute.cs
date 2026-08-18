
// Type: Intermech.Interfaces.SessionGuardAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Позволяет задавать флаги защиты для методов и свойств объектов сервера приложений от использования вне SessionKeeper.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, Inherited = true)]
    [Serializable]
    public class SessionGuardAttribute : Attribute
    {
      private readonly SessionGuardMode mode;

      /// <summary>Создает объект.</summary>
      /// <param name="mode">Режим защиты для метода и свойства объекта сервера приложений от использования вне SessionKeeper</param>
      public SessionGuardAttribute(SessionGuardMode mode) => this.mode = mode;

      /// <summary>
      /// Возвращает режим защиты для метода и свойства объекта сервера приложений от использования вне SessionKeeper.
      /// </summary>
      public SessionGuardMode Mode => this.mode;
    }
}
