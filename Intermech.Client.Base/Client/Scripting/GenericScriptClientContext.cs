
// Type: Intermech.Client.Scripting.GenericScriptClientContext
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
    /// Базовый объект контекста для сценариев. Через него сценарии могут обращаться к API основного приложения.
    /// Сервисы основного приложения доступны в виде свойств контекста.
    /// </summary>
    /// <remarks>
    /// Реализация этого объекта и всех сервисов, доступных из него, должны быть thread safe и
    /// поддерживать вызовы через remoting (т.е. наследоваться от MarshalByRefObject).
    /// </remarks>
    internal abstract class GenericScriptClientContext : LongLifeObject
    {
      private IMetaDataHelper metaDataHelper;

      protected GenericScriptClientContext(IMetaDataHelper metaDataHelper)
      {
        this.metaDataHelper = metaDataHelper != null ? metaDataHelper : throw new ArgumentNullException(nameof (metaDataHelper));
      }

      public IMetaDataHelper MetaDataHelper
      {
        [DebuggerStepThrough] get => this.metaDataHelper;
      }

      [Obsolete("Вместо обращения к этому свойству в ScriptContext следует объявить и использовать аналогичное свойство в классе Script", true)]
      public IOutputView OutputView
      {
        [DebuggerStepThrough] get => throw this.CreateObsoletePropertyAccessException();
      }

      [Obsolete("Вместо обращения к этому свойству в ScriptContext следует объявить и использовать аналогичное свойство в классе Script", true)]
      public IAuthFilesService AuthFilesService
      {
        [DebuggerStepThrough] get => throw this.CreateObsoletePropertyAccessException();
      }

      private Exception CreateObsoletePropertyAccessException()
      {
        throw new NotSupportedException("Вместо обращения к этому свойству в ScriptContext следует объявить и использовать аналогичное свойство в классе Script");
      }
    }
}
