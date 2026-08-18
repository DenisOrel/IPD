
// Type: Intermech.Client.ClientRemotingConfigurator
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Remoting;
using Intermech.Remoting.Optimized;
using System;
using System.Runtime.Remoting;


namespace Intermech.Client
{
    /// <summary>Менеджер конфигурирования Remoting для клиента IPS.</summary>
    public sealed class ClientRemotingConfigurator
    {
      private string originalFilename;
      private bool ensureSecurity;
      private string[] supportedChannels;

      /// <summary>Создает объект с параметрами по умолчанию.</summary>
      public ClientRemotingConfigurator()
        : this(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false)
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="filename">Имя файла с конфигурацией Remoting. Обычно это app.config приложения</param>
      /// <param name="ensureSecurity">Флаг установки безопасных сетевых соединений</param>
      public ClientRemotingConfigurator(string filename, bool ensureSecurity)
      {
        this.originalFilename = filename != null ? filename : throw new ArgumentNullException(nameof (filename));
        this.ensureSecurity = ensureSecurity;
        this.supportedChannels = new string[2]{ "tcp", "http" };
      }

      /// <summary>Конфигурирует Remoting.</summary>
      public void Configure()
      {
        this.ClearInternal();
        try
        {
          RemotingXmlDataHack remotingXmlDataHack = new RemotingXmlDataHack(this.originalFilename);
          foreach (string supportedChannel in this.supportedChannels)
          {
            if (remotingXmlDataHack.HasChannelDefinition(supportedChannel))
            {
              remotingXmlDataHack.ReplaceClientFormatter(supportedChannel, "binary", typeof (BinaryClientFormatterSinkPatcherProvider));
              remotingXmlDataHack.ReplaceServerFormatter(supportedChannel, "binary", typeof (OptimizedBinaryServerFormatterSinkProvider));
            }
          }
          RemotingConfiguration.Configure(remotingXmlDataHack.ToFile(), this.ensureSecurity);
        }
        catch
        {
          this.ClearInternal();
          throw;
        }
      }

      private void ClearInternal()
      {
      }
    }
}
