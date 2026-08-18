
// Type: Intermech.Search.EditingContexts.EditingContextsModule
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Configuration;


namespace Intermech.Search.EditingContexts
{
    public sealed class EditingContextsModule
    {
      public void Load()
      {
        ServiceLocator.Get<IConfigurationOptionInfoProvider>().Register(new ConfigurationOptionInfo(typeof (bool))
        {
          Description = "Предлагать включение режима автоматического пополнения текущего контекста редактирования при входе в систему",
          DisplayName = "Предлагать включение режима автоматического пополнения текущего контекста редактирования при входе в систему",
          Key = EditingContextsConfigurationOptionKyes.ShowEditingContextAutoRefillDialogOnClientEnter,
          Mode = DBConfigMode.UserOnly,
          Page = "Система/Подбор версий",
          TypeConverter = typeof (YesNoBooleanConverter)
        });
      }
    }
}
