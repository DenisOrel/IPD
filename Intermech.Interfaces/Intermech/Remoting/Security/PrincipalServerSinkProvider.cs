
// Type: Intermech.Remoting.Security.PrincipalServerSinkProvider
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Security.Permissions;


namespace Intermech.Remoting.Security
{
    /// <summary>
    /// Реализует провайдер для серверного канального приемника.
    /// </summary>
    public sealed class PrincipalServerSinkProvider : IServerChannelSinkProvider
    {
      private IServerChannelSinkProvider nextProvider;

      /// <summary>Создает объект.</summary>
      /// <param name="properties">Свойства</param>
      /// <param name="providerData">Данные провайдера</param>
      public PrincipalServerSinkProvider(IDictionary properties, ICollection providerData)
      {
      }

      /// <summary>
      /// Возвращает или задает следующий провайдер канального приемника в цепочке.
      /// </summary>
      public IServerChannelSinkProvider Next
      {
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] get
        {
          return this.nextProvider;
        }
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)] set
        {
          this.nextProvider = value;
        }
      }

      /// <summary>Создает канальный приемник.</summary>
      /// <param name="channel">Канал</param>
      /// <returns>Серверный канальный приемник</returns>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public IServerChannelSink CreateSink(IChannelReceiver channel)
      {
        return (IServerChannelSink) new PrincipalServerSink(this.nextProvider.CreateSink(channel));
      }

      /// <summary>
      /// Заполняет данные канала сведениями, специфическими для этого типа канальных приемников.
      /// </summary>
      /// <param name="channelData">Данные канала</param>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public void GetChannelData(IChannelDataStore channelData)
      {
      }
    }
}
