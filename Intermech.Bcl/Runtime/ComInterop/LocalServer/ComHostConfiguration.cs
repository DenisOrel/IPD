
// Type: Intermech.Runtime.ComInterop.LocalServer.ComHostConfiguration
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>Описывает конфигурацию COM-сервера.</summary>
    public sealed class ComHostConfiguration
    {
      private ComServer comServer;

      internal ComHostConfiguration(ComServer comServer) => this.comServer = comServer;

      /// <summary>Возвращает true, если поддержка COM включена.</summary>
      public bool ComSupportActive => this.comServer.IsActive;
    }
}
