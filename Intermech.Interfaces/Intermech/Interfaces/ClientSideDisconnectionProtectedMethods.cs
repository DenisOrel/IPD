
// Type: Intermech.Interfaces.ClientSideDisconnectionProtectedMethods
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Remoting;
using System;
using System.Collections.Concurrent;
using System.Reflection;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Защите подлежать только методы IUserSession и других объектов, которые используют
    /// <see cref="T:Intermech.Interfaces.IUserSession" /> неявно. Связано это с тем, что <see cref="T:Intermech.Interfaces.IUserSession" /> и
    /// связанные объекты не являются thread safe, а односторонние ошибки remoting приводят к
    /// незащищенному многопоточному доступу к этим объектам. Защищать <see cref="T:Intermech.Interfaces.IMServer" /> и
    /// его компоненты не требуется, так как это thread safe singleton. Более того, это будет
    /// мешать работе механизма переподключения при обрыве подключения к серверу приложений.
    /// </summary>
    internal sealed class ClientSideDisconnectionProtectedMethods
    {
      private ConcurrentDictionary<MethodBase, bool> cache;
      private Func<MethodBase, bool> addToCacheMethod;

      public ClientSideDisconnectionProtectedMethods()
      {
        this.cache = new ConcurrentDictionary<MethodBase, bool>();
        this.addToCacheMethod = new Func<MethodBase, bool>(this.CanProtectSlow);
      }

      public bool CanProtect(MethodBase method) => this.cache.GetOrAdd(method, this.addToCacheMethod);

      private bool CanProtectSlow(MethodBase method)
      {
        ClientSideDisconnectionProtectionAttribute customAttribute1 = method.GetCustomAttribute<ClientSideDisconnectionProtectionAttribute>(true);
        if (customAttribute1 != null)
          return customAttribute1.Enabled;
        ClientSideDisconnectionProtectionAttribute customAttribute2 = method.DeclaringType.GetCustomAttribute<ClientSideDisconnectionProtectionAttribute>(true);
        return customAttribute2 == null || customAttribute2.Enabled;
      }
    }
}
