
// Type: Intermech.Interfaces.ICSharpScriptContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Объект контекста для C#-сценариев, выполняемых в изолированном окружении.
    /// Через этот объект сценарии могут обращаться к API основного приложения.
    /// </summary>
    /// <remarks>
    /// Сервисы основного приложения доступны в виде свойств контекста.
    /// </remarks>
    public interface ICSharpScriptContext
    {
      /// <summary>Сервис исполнителя скриптов.</summary>
      ICSharpScriptExecutor ScriptExecutor { get; }

      /// <summary>Сервис кэша метаданных базы данных.</summary>
      IMetaDataHelper MetaDataHelper { get; }
    }
}
