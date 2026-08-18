
// Type: Intermech.Interfaces.LongLifeObject
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый класс для singleton-сервисов, доступных через remoting.
    /// Время жизни таких объектов устанавливается в бесконечность.
    /// Если такой объект требуется отключить от remoting, то необходимо
    /// явно вызывать метод <see cref="M:System.Runtime.Remoting.RemotingServices.Disconnect(System.MarshalByRefObject)" />.
    /// </summary>
    public class LongLifeObject : MarshalByRefObject
    {
      /// <summary>Инициализирует время жизни объекта в бесконечность.</summary>
      /// <returns>null</returns>
      public override object InitializeLifetimeService() => (object) null;
    }
}
