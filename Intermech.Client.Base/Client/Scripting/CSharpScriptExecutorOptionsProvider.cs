
// Type: Intermech.Client.Scripting.CSharpScriptExecutorOptionsProvider
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Configuration;
using Intermech.Interfaces.Client;
using Intermech.Scripting.CSharp.Hosting;
using System;


namespace Intermech.Client.Scripting
{
    internal sealed class CSharpScriptExecutorOptionsProvider : ScriptExecutorOptionsProvider
    {
      private IMServerService imServerService;

      public CSharpScriptExecutorOptionsProvider(IMServerService imServerService)
      {
        this.imServerService = imServerService != null ? imServerService : throw new ArgumentNullException(nameof (imServerService));
      }

      protected override bool GetLogAllInvocationsOption(string optionName, bool defaultValue)
      {
        return AppSettingsHelper.ParseBoolean(this.imServerService.GetAppConfigurationService().GetConfigurationOption(optionName), defaultValue);
      }
    }
}
