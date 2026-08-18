
// Type: Intermech.Scripting.CSharp.Debugging.ICSharpDebugExecutor
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Scripting.Common.Debugging;
using System;
using System.Collections.Generic;


namespace Intermech.Scripting.CSharp.Debugging
{
    /// <summary>
    /// Дополнительный интерфейс сервиса выполнения C#-сценариев, используемый для отладки сценариев.
    /// Реализация обязана быть thread safe.
    /// </summary>
    public interface ICSharpDebugExecutor : IDebugExecutor
    {
      /// <summary>
      /// Создает и возвращает специальную сессию сервера приложений для режима отладки сценариев.
      /// </summary>
      /// <param name="clientToken">Токен клиента</param>
      /// <returns>Сессия сервера приложений</returns>
      Tuple<IUserSession, string> CreateDebugSystemSession(int clientToken);

      /// <summary>
      /// Возвращает все сборки сервера приложений для автодополнения кода сценариев.
      /// </summary>
      /// <param name="clientToken">Токен клиента</param>
      /// <returns>Список путей к сборкам сервера приложений</returns>
      ICollection<string> GetAssembliesForAutocompletion(int clientToken);
    }
}
