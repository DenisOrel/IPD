
// Type: Intermech.Remoting.IServerObjectWrapper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Remoting
{
    /// <summary>
    /// Позволяет получить доступ к серверному объекту из клиентской обертки.
    /// </summary>
    public interface IServerObjectWrapper
    {
      /// <summary>Возвращает ссылку на серверный объект.</summary>
      /// <returns>Ссылка на серверный объект</returns>
      MarshalByRefObject GetServerObject();
    }
}
