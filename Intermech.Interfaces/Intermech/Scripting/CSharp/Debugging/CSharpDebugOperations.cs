
// Type: Intermech.Scripting.CSharp.Debugging.CSharpDebugOperations
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Scripting.Common;
using Intermech.Scripting.Common.Debugging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace Intermech.Scripting.CSharp.Debugging
{
    /// <summary>
    /// Класс утилит, унифицирующий отладку клиентских и серверных C#-сценариев.
    /// Реализация класса является thread safe.
    /// </summary>
    public sealed class CSharpDebugOperations
    {
      /// <summary>
      /// Возвращает все сборки текущего приложения, которые можно использовать для автодополнения кода сценариев.
      /// </summary>
      /// <param name="clientToken">Токен клиента</param>
      /// <returns>Список путей к сборкам текущего приложения</returns>
      public ICollection<string> GetAssembliesForAutocompletion()
      {
        HashSet<string> collection = new HashSet<string>((IEqualityComparer<string>) PathUtils.CurrentPathComparer);
        AppDomain currentDomain = AppDomain.CurrentDomain;
        foreach (Assembly assembly in currentDomain.GetAssemblies())
        {
          if (!assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location))
            collection.Add(assembly.Location);
        }
        foreach (string file in Directory.GetFiles(currentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly))
        {
          if (!collection.Contains(file))
            collection.Add(file);
        }
        return (ICollection<string>) CollectionUtils.ToArray<string>((ICollection<string>) collection);
      }

      /// <summary>Выполняет сценарий в режиме отладки.</summary>
      /// <param name="localExecutor">Локальный исполнитель сценариев текущего приложения</param>
      /// <param name="scriptCode">Код сценария</param>
      /// <param name="arguments">Аргументы вызова сценария</param>
      /// <returns>Результат выполнения сценария</returns>
      /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="localExecutor" /> не должен быть равен null; параметр <paramref name="scriptCode" /> не должен быть равен null; параметр <paramref name="arguments" /> не должен быть равен null</exception>
      public DebugExecuteResult DebugExecute(
        ICSharpScriptExecutor localExecutor,
        string scriptCode,
        object[] arguments)
      {
        if (localExecutor == null)
          throw new ArgumentNullException(nameof (localExecutor));
        if (scriptCode == null)
          throw new ArgumentNullException(nameof (scriptCode));
        if (arguments == null)
          throw new ArgumentNullException(nameof (arguments));
        SimpleOutputStream debugStream = new SimpleOutputStream();
        CSharpScriptInvocationOptions options = new CSharpScriptInvocationOptions(true, (IScriptOutputStream) debugStream);
        object obj = (object) null;
        Exception exception = (Exception) null;
        try
        {
          obj = localExecutor.Execute(scriptCode, options, arguments);
        }
        catch (Exception ex)
        {
          exception = ex;
        }
        return new DebugExecuteResult()
        {
          ReturnValue = obj,
          Exception = exception,
          DebugOutput = debugStream.ToArray()
        };
      }
    }
}
