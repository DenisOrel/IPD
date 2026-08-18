
// Type: Intermech.Scripting.CSharp.Hosting.CSharpScriptCodeAnalyzer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Text.RegularExpressions;


namespace Intermech.Scripting.CSharp.Hosting
{
    /// <summary>
    /// Анализатор исходного кода C#-сценария. Позволяет выяснить структуру и особенности выполнения сценария до компиляции и собстенно выполнения.
    /// Подобный анализ необходим для выбора одного из нескольких существующих исполнителей сценариев.
    /// Реализация класса является thread safe.
    /// </summary>
    public sealed class CSharpScriptCodeAnalyzer
    {
      private Regex scriptContextPropertyPattern;

      /// <summary>Создает объект.</summary>
      public CSharpScriptCodeAnalyzer()
      {
        this.scriptContextPropertyPattern = new Regex("^\\s*public\\s+(ICSharpScriptContext|ICSharpScriptClientContext|ICSharpScriptServerContext) ScriptContext(\\s+|$|{)", RegexOptions.Multiline | RegexOptions.Compiled);
      }

      /// <summary>
      /// Проверяет, может ли сценарий быть выполнен в изолированном окружении.
      /// </summary>
      /// <param name="scriptCode">Код сценария</param>
      /// <returns>true - код сценария содержит свойство ScriptContext и может быть выполнен в изолированном окружении</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="scriptCode" /> не должен быть равен null</exception>
      public bool CanExecuteInSandbox(string scriptCode)
      {
        return scriptCode != null ? this.scriptContextPropertyPattern.IsMatch(scriptCode) : throw new ArgumentNullException(nameof (scriptCode));
      }
    }
}
