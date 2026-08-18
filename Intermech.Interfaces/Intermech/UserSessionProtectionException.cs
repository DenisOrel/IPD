
// Type: Intermech.UserSessionProtectionException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Исключение генерится в методах, защищающих класс юзерской сессии от неверного использования
    /// </summary>
    [Serializable]
    public class UserSessionProtectionException : KernelException
    {
      public UserSessionProtectionException(string message)
        : base(message)
      {
      }

      public UserSessionProtectionException()
      {
      }

      protected UserSessionProtectionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
