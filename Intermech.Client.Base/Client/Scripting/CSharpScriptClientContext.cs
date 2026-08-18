
// Type: Intermech.Client.Scripting.CSharpScriptClientContext
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Diagnostics;


namespace Intermech.Client.Scripting
{
    /// <summary>
    /// Объект контекста для C#-сценариев, выполняемых в изолированном окружении.
    /// Через этот объект сценарии могут обращаться к API основного приложения.
    /// Сервисы основного приложения доступны в виде свойств контекста.
    /// </summary>
    /// <remarks>
    /// Реализация этого объекта и всех сервисов, доступных из него, должны быть thread safe и
    /// поддерживать вызовы через remoting (т.е. наследоваться от MarshalByRefObject).
    /// </remarks>
    internal sealed class CSharpScriptClientContext : 
      GenericScriptClientContext,
      ICSharpScriptClientContext,
      ICSharpScriptContext
    {
      private Lazy<ICSharpScriptExecutor> scriptExecutor;

      public CSharpScriptClientContext(
        IMetaDataHelper metaDataHelper,
        Lazy<ICSharpScriptExecutor> scriptExecutor)
        : base(metaDataHelper)
      {
        this.scriptExecutor = scriptExecutor != null ? scriptExecutor : throw new ArgumentNullException(nameof (scriptExecutor));
      }

      public ICSharpScriptExecutor ScriptExecutor
      {
        [DebuggerStepThrough] get => this.scriptExecutor.Value;
      }
    }
}
