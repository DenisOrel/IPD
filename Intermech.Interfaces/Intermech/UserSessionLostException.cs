
// Type: Intermech.UserSessionLostException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class UserSessionLostException : KernelException
    {
      /// <summary>Создает объект.</summary>
      public UserSessionLostException()
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="message">Сообщение</param>
      public UserSessionLostException(string message)
        : base(message)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      protected UserSessionLostException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="info"></param>
      /// <param name="context"></param>
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
      }
    }
}
