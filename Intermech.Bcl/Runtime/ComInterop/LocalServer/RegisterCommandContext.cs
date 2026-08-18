
// Type: Intermech.Runtime.ComInterop.LocalServer.RegisterCommandContext
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal sealed class RegisterCommandContext
    {
      private static RegisterCommandContext globalContext;

      public RegisterCommandContext(
        ComPluginRegistrationService registrationService,
        RegisterComPluginContext pluginContext)
      {
        if (registrationService == null)
          throw new ArgumentNullException(nameof (registrationService));
        if (pluginContext == null)
          throw new ArgumentNullException(nameof (pluginContext));
        this.RegistrationService = registrationService;
        this.PluginContext = pluginContext;
      }

      public ComPluginRegistrationService RegistrationService { get; private set; }

      public RegisterComPluginContext PluginContext { get; private set; }

      /// <summary>
      /// Возвращает или задает глобальный экземпляр контекста выполнения команды.
      /// Это единственный способ передать дополнительные параметры в метод регистрации, реализуемый самим COM-объектом.
      /// </summary>
      public static RegisterCommandContext Global
      {
        get => RegisterCommandContext.globalContext;
        set => RegisterCommandContext.globalContext = value;
      }

      public static RegisterCommandContext GetGlobalContextOrFail()
      {
        return RegisterCommandContext.Global ?? throw new InvalidOperationException("The property RegisterCommandContext.Global must not be null. This COM object can be registered only in the context of a LocalServer executable.");
      }
    }
}
