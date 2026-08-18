
// Type: Intermech.Runtime.ComInterop.LocalServer.UnregisterCommandContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal sealed class UnregisterCommandContext
    {
      private static UnregisterCommandContext globalContext;

      public UnregisterCommandContext(
        ComPluginRegistrationService registrationService,
        UnregisterComPluginContext pluginContext)
      {
        if (registrationService == null)
          throw new ArgumentNullException(nameof (registrationService));
        if (pluginContext == null)
          throw new ArgumentNullException(nameof (pluginContext));
        this.RegistrationService = registrationService;
        this.PluginContext = pluginContext;
      }

      public ComPluginRegistrationService RegistrationService { get; private set; }

      public UnregisterComPluginContext PluginContext { get; private set; }

      /// <summary>
      /// Возвращает или задает глобальный экземпляр контекста выполнения команды.
      /// Это единственный способ передать дополнительные параметры в метод отмены регистрации, реализуемый самим COM-объектом.
      /// </summary>
      public static UnregisterCommandContext Global
      {
        get => UnregisterCommandContext.globalContext;
        set => UnregisterCommandContext.globalContext = value;
      }

      public static UnregisterCommandContext GetGlobalContextOrFail()
      {
        return UnregisterCommandContext.Global ?? throw new InvalidOperationException("The property UnregisterCommandContext.Global must not be null. This COM object can be unregistered only in the context of a LocalServer executable.");
      }
    }
}
