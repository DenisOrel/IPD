
// Type: Intermech.Remoting.ClientSideDisconnectionProtectionAttribute
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Remoting
{
    /// <summary>
    /// Позволяет управлять защитой от односторонних ошибок remoting на уровне отдельных методов, свойств или типов.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, Inherited = true)]
    public sealed class ClientSideDisconnectionProtectionAttribute : Attribute
    {
      private bool enabled;

      /// <summary>Создает атрибут.</summary>
      /// <param name="enabled">Признак включения или выключения защиты</param>
      public ClientSideDisconnectionProtectionAttribute(bool enabled) => this.enabled = enabled;

      /// <summary>Возвращает признак включения или выключения защиты.</summary>
      public bool Enabled
      {
        [DebuggerStepThrough] get => this.enabled;
      }
    }
}
