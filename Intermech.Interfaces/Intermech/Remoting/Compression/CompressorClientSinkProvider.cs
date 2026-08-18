
// Type: Intermech.Remoting.Compression.CompressorClientSinkProvider
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Configuration;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Security.Permissions;


namespace Intermech.Remoting.Compression
{
    /// <summary>
    /// Реализует провайдер для клиентского канального приемника, выполняющего сжатие сетевого трафика.
    /// </summary>
    public sealed class CompressorClientSinkProvider : IClientChannelSinkProvider
    {
      private IClientChannelSinkProvider nextProvider;
      private bool enabled;

      /// <summary>Создает объект.</summary>
      /// <param name="properties">Свойства</param>
      /// <param name="providerData">Данные провайдера</param>
      public CompressorClientSinkProvider(IDictionary properties, ICollection providerData)
      {
        if (properties == null)
          return;
        this.enabled = AppSettingsHelper.ParseBoolean(properties[(object) nameof (enabled)] as string, false);
      }

      /// <summary>Создает объект.</summary>
      public CompressorClientSinkProvider()
      {
      }

      /// <summary>Включает и выключает сжатие сетевого трафика.</summary>
      public bool Enabled
      {
        get => this.enabled;
        set => this.enabled = value;
      }

      /// <summary>
      /// Возвращает или задает следующий провайдер канального приемника в цепочке.
      /// </summary>
      public IClientChannelSinkProvider Next
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
      /// <param name="url">Строка подключения</param>
      /// <param name="remoteChannelData">Данные канала</param>
      /// <returns>Клиентский канальный приемник</returns>
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
      public IClientChannelSink CreateSink(
        IChannelSender channel,
        string url,
        object remoteChannelData)
      {
        return (IClientChannelSink) new CompressorClientSink(this.nextProvider.CreateSink(channel, url, remoteChannelData), this.enabled);
      }
    }
}
