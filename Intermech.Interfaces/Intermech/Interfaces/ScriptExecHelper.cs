
// Type: Intermech.Interfaces.ScriptExecHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Scripting;
using System;


namespace Intermech.Interfaces
{
    /// <summary>Класс хеплер для вызова C#-сценариев</summary>
    public static class ScriptExecHelper
    {
      private const string _serviceNotFound = "Отсутствует сервис выполнения сценариев C#";

      /// <summary>
      /// Выполняет метод Execute из кода code, используя аргументы arguments. При возникновении исключения возвращает текст ошибки.
      /// </summary>
      /// <param name="code"></param>
      /// <param name="options"></param>
      /// <param name="arguments">Аргументы вызова метода</param>
      /// <returns></returns>
      public static string IsolatedExecScript(
        string code,
        CSharpScriptInvocationOptions options,
        params object[] arguments)
      {
        ICSharpScriptExecutor service = ApplicationServices.Container.GetService<ICSharpScriptExecutor>();
        if (service == null)
          return "Отсутствует сервис выполнения сценариев C#";
        try
        {
          service.Execute(code, options, arguments);
          return string.Empty;
        }
        catch (Exception ex)
        {
          switch (ex)
          {
            case ISimpleMessageException _:
              throw;
            case AbortException _:
              throw;
            case ScriptInvocationException _:
              if (ex.InnerException == null)
                return ExceptionServices.GetExtendedExceptionText(ex);
              if (ex.InnerException is ISimpleMessageException)
                throw ex.InnerException;
              return !(ex.InnerException is AbortException) ? ExceptionServices.GetExtendedExceptionText(ex.InnerException) : throw ex.InnerException;
            default:
              return ExceptionServices.GetExtendedExceptionText(ex);
          }
        }
      }
    }
}
