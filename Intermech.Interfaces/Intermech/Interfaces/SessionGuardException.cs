
// Type: Intermech.Interfaces.SessionGuardException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Сбрасывается механизмом защиты объектов сервера приложений от использования вне SessionKeeper.
    /// </summary>
    [Serializable]
    public class SessionGuardException : Exception
    {
      /// <summary>Создает объект.</summary>
      public SessionGuardException()
        : base("Использование объектов сервера приложений вне SessionKeeper строжайше запрещено!")
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="info">Сериализованное представление объекта</param>
      /// <param name="context">Контекст сериализации</param>
      protected SessionGuardException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
